using SuperMovie.Container.Vip.Entity;
using SuperMovie.Db.Vip.Model;
using SuperMovie.Db.Vip.Operation;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Container.Vip.Provider
{
    public class VipProvider : IVipProvider
    {
        IDatabase Db;
        public VipProvider(IDatabase db)
        {
            this.Db = db;
        }
        public IVipEntity? CreateVip(long level, double discount, double monthprice)
        {
            IVipOperation vipOperation = new VipOperation(Db);
            IVipModel model = new VipModel(level, discount,monthprice);
            var flag = vipOperation.CreateVip(model);
            if (flag == true)
            {
                IVipEntity entity = new VipEntity(level,Db);
                return entity;
            }
            else return null;
        }
        public IVipEntity? GetVip(long level)
        {
            IVipOperation vipOperation = new VipOperation(Db);
            var dbvip = vipOperation.GetVip(level);
            if (dbvip == null)
                return null;
            else
            {
                IVipEntity entity = new VipEntity(dbvip.Level, Db);
                return entity;
            }
        }
        public List<IVipEntity> GetAllVip()
        {
            IVipOperation vipOperation = new VipOperation(Db);
            var result = vipOperation.GetAllVip();
            List<IVipEntity> ret = new List<IVipEntity>();
            foreach (var item in result)
            {
                var vipEntity = new VipEntity(item.Level,Db);
                ret.Add(vipEntity);
            }
            return ret;
        }
    }
}
