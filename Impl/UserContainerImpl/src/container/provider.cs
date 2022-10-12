using SuperMovie.Container.User.Entity;
using SuperMovie.Container.Vip.Entity;
using SuperMovie.Db.User.Model;
using SuperMovie.Db.User.Operation;
using SuperMovie.Db.Vip.Operation;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Util;
using DbMiddleware;
namespace SuperMovie.Container.User.Provider
{
    public class UserProvider : IUserProvider
    {
        IDatabase Db;
        public Func<IOrderProvider> orderProviderF;
        public UserProvider( Func<IOrderProvider> orderProviderF, IDatabase db)
        {
            this.orderProviderF = orderProviderF;
            this.Db = db;
        }
        public IUserEntity? CreateUser(long id, string pwd, IVipEntity vip, DateTime vipLevelExpireTime)
        {
            int up = 0, low = 0;
            if (pwd.Length >= 8)
            {
                foreach (char c in pwd)
                {
                    if (c >= 'a' && c <= 'z')
                        up++;
                    if (c >= 'A' && c <= 'Z')
                        low++;
                }
                if (up > 0 && low > 0)
                {
                    IUserOperation userOperation = new UserOperation(Db);
                    Hasher hasher = new Hasher();
                    var hpwd = hasher.GetHash(pwd);
                    IUserModel user = new UserModel(id, hpwd, vip.Level, vipLevelExpireTime);
                    var flag = userOperation.CreateUser(user);
                    if (flag)
                    {
                        IUserEntity entity = new UserEntity(id, this.orderProviderF, Db);
                        return entity;
                    }
                    else return null;
                }
                else return null;
            }
            else return null;
        }
        public IUserEntity? GetUser(long id)
        {
            IUserOperation userOperation = new UserOperation(Db);
            var result = userOperation.GetUser(id);//取出数据库中的user
            if (result != null)
            {
                IVipOperation vipOperation = new VipOperation(Db);
                var vip = vipOperation.GetVip(result.VipLevel);//取出数据库中vip
                if (vip != null)
                {
                    IUserEntity entity = new UserEntity(id, this.orderProviderF,Db);
                    return entity;
                }
                else return null;
            }
            else return null;
        }
        public IUserEntity? IsIdMatchPwd(long id, string pwd)
        {
            Hasher hasher = new Hasher();
            IUserOperation userOperation = new UserOperation(Db);
            var result = userOperation.GetUser(id);

            if (result != null)
            {
                var flag = hasher.TextIsHash(pwd, result.PwdHash);
                if (flag)
                {
                    return this.GetUser(id);
                }
                else return null;
            }
            else return null;
        }
        public List<IUserEntity> GetAllUser()
        {
            IUserOperation userOperation = new UserOperation(Db);
            var result1 = userOperation.GetAllUser();
            var ret = new List<IUserEntity>();
            foreach (var item in result1)
            {
                IUserEntity userEntity = new UserEntity(item.Id, this.orderProviderF,Db);
                ret.Add(userEntity);
            }
            return ret;
        }
    }
}
