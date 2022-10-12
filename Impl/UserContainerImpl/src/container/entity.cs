using SuperMovie.Container.Vip.Entity;
using SuperMovie.Db.User.Operation;
using SuperMovie.Db.Vip.Operation;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Container.Order.Entity;
using SuperMovie.Container.Vip.Provider;
using SuperMovie.Util;
using SuperMovie.Container.User.Provider;
using DbMiddleware;

namespace SuperMovie.Container.User.Entity
{
    public class UserEntity : IUserEntity
    {
        public IDatabase Db;
        public long id;
        private Func<IOrderProvider> orderProviderF;
        public bool Drop()
        {
            UserOperation op = new UserOperation(Db);
            if (this.ClearOrder())
            {
                var flag = op.DeleteUser(Id);
                if (flag == true)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public long Id
        {
            get
            { return id; }
        }
        public bool IsValid()
        {
            IUserOperation userOperation = new UserOperation(Db);
            var user = userOperation.GetUser(this.Id);
            if (user == null)
                return false;
            else return true;
        }
        public bool ResetPwd(string oldPwd, string newPwd)
        {
            int up = 0,low = 0;
            if (newPwd.Length >= 8)
            {
                foreach (char c in newPwd)
                {
                    if (c >= 'a' && c <= 'z')
                        up++;
                    if (c >= 'A' && c <= 'Z')
                        low++;
                }
                if (up > 0 && low > 0)
                {
                    if (oldPwd != newPwd)
                    {
                        Hasher hasher = new Hasher();
                        var user = new UserOperation(Db).GetUser(this.Id);
                        if (user != null)
                        {
                            if (hasher.TextIsHash(oldPwd, user.PwdHash))
                            {
                                var newhpd = hasher.GetHash(newPwd);
                                var updateuser = new UserOperation(Db);
                                updateuser.UpdateUserPwdHash(Id, newhpd);
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false; 
        }
        public IVipEntity? Vip
        {
            get
            {
                IVipProvider vipProvider = new VipProvider(Db);
                IUserOperation userpOperation = new UserOperation(Db);
                var userModel = userpOperation.GetUser(this.Id);
                if (userModel != null)
                {
                    if (userModel.VipLevelExpireTime < DateTime.Now)
                    {
                        var update_result = userpOperation.UpdateUserVipLevel(userModel.Id, 0);
                        if (update_result)
                            userModel.VipLevel = 0;
                    }
                    var vipentity = vipProvider.GetVip(userModel.VipLevel);
                    if (vipentity != null )
                    {
                        return vipentity;
                    }
                    else return null;
                }
                else return null;
            }
            set
            {
                IVipOperation vipOperation = new VipOperation(Db);
                if (value != null)
                {
                    var vip = vipOperation.GetVip(value.Level);
                    if (vip != null)
                        vipOperation.UpdateVipDiscount(value.Level, value.Discount);
                }

            }
        }
        public DateTime? VipLevelExpireTime
        {
            get
            {
                IUserOperation userOperation = new UserOperation(Db);
                var user = userOperation.GetUser(this.Id);
                if (user != null)
                {
                    return user.VipLevelExpireTime;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    IUserOperation userOperation = new UserOperation(Db);
                    var result = userOperation.UpdateUserVipLevelExpireTime(Id, (DateTime)value);
                }
            }
        }
        public List<IOrderEntity> Orders
        {
            get
            {
                var op=orderProviderF();
                List<IOrderEntity> list = op.GetAllOrder();
                var ret = new List<IOrderEntity>();
                foreach (var item in list)
                {
                    if (item.User != null && item.User.Id == this.Id)
                        {
                            ret.Add(item);
                        }
                    }
                return ret;
                
            }
        }
        public bool ClearOrder()
        {
            if (this.Orders != null)
            {
                foreach (var item in this.Orders)
                {
                    item.User = new UserProvider(this.orderProviderF,Db).GetUser(100);
                }
                return true;
            }
            else return false;
        }
        public double GetIfRefundVipThenAmount()
        {
            IUserOperation userOperation = new UserOperation(Db);
            var user = userOperation.GetUser(this.Id);
            if (user != null)
            {
                IVipOperation vipOperation = new VipOperation(Db);
                var vip = vipOperation.GetVip(user.VipLevel);
                if (vip != null)
                {
                    if (DateTime.Now.Day < user.VipLevelExpireTime.Day)
                    {
                        double ret = vip.MonthPrice * (user.VipLevelExpireTime.Day - DateTime.Now.Day) / 30.0;
                        return ret;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }
        internal UserEntity(long id, Func<IOrderProvider> orderProviderF,IDatabase db)
        {
            this.id = id;
            this.orderProviderF = orderProviderF;
            this.Db = db;
        }
    }
}
