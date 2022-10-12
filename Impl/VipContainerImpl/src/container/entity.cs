using SuperMovie.Db.Vip.Operation;
using DbMiddleware;
using DbMiddlewarePostgresImpl;
namespace SuperMovie.Container.Vip.Entity
{
    public class VipEntity : IVipEntity
    {
        IDatabase Db;
        public long level;
        internal VipEntity(long level, IDatabase db)
        {
            this.level = level;
            this.Db = db;
        }
        public bool Drop()
        {
            IVipOperation vip = new VipOperation(Db);
            var flag = vip.DeleteVip(this.Level);
            if (flag == true)
                return true;
            else return false;
        }
        public long Level
        {
            get
            {
                return level;
            }
        }
        public double Discount
        {
            get
            {
                IVipOperation vipOperation = new VipOperation(Db);
                var vip = vipOperation.GetVip(this.Level);
                if (vip == null)
                    return 0;
                else
                    return vip.Discount;
            }
            set
            {
                IVipOperation vipOperation = new VipOperation(Db);
                var flag1 = vipOperation.GetVip(Level);
                if (flag1 != null)
                {
                    var flag2 = vipOperation.UpdateVipDiscount(this.Level, value);
                }

            }
        }
        public double MonthPrice { 
            get
            {
                IVipOperation vipOperation = new VipOperation(Db);
                var flag = vipOperation.GetVip(this.Level);
                if (flag != null)
                    return flag.MonthPrice;
                else return 0;
            }
            set
            {
                if (value>0)
                {
                    IVipOperation vipOperation = new VipOperation(Db);
                    var flag1 = vipOperation.GetVip(this.Level);
                    if (flag1 != null)
                    {
                        var flag2 = vipOperation.UpdateVipMonthPrice(this.Level, value);
                    }
                }
            }
        }
        public bool IsValid()
        {
            IVipOperation vipOperation = new VipOperation(Db);
            var result = vipOperation.GetVip(this.Level);
            if (result != null)
            {
                if (this.Discount == result.Discount && this.MonthPrice == result.MonthPrice)
                    return true;
                else return false;
            }
            else return false;
        }
    }
}
