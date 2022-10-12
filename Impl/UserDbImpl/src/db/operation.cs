using DbMiddleware;
using DbMiddlewarePostgresImpl;
using SuperMovie.Db.User.Model;
namespace SuperMovie.Db.User.Operation
{
    public class UserOperation : IUserOperation
    {
        IDatabase Db;
        public UserOperation (IDatabase db)
        {
            this.Db = db;
        }
        public bool CreateUser(IUserModel model)
        {
            var sql = "INSERT INTO default_schema.user (user_id,user_pwd_hash,user_vip_level,user_vip_level_expire_time) VALUES(:user_id,:user_pwd_hash,:user_vip_level,:user_vip_level_expire_time)";
            var flag = Db.Query(sql, 1, new[] { ("user_id", (object)model.Id), ("user_pwd_hash", (object)model.PwdHash), ("user_vip_level", (object)model.VipLevel), ("user_vip_level_expire_time", (object)model.VipLevelExpireTime) });
            if (flag == 1) return true;
            else return false;
        }
        public bool DeleteUser(long userId)
        {
            var sql = "DELETE FROM default_schema.user where user_id=:userID";
            var flag = Db.Query(sql, 1, new[] { ("userID", (object)userId) });
            if (flag == 1) return true;
            else return false;
        }
        public List<IUserModel> GetAllUser()
        {
            var sql = "SELECT * FROM default_schema.user";
            var result = Db.QueryForTable(sql, null);

            var ret = new List<IUserModel>();
            foreach (var row in result)
            {
                var model = new UserModel(
                    (long)row["user_id"],
                    (string)row["user_pwd_hash"],
                    (long)row["user_vip_level"],
                    (DateTime)row["user_vip_level_expire_time"]);
                ret.Add(model);
            }
            return ret;
        }
        public bool IsPwdHashValid(long userId, string pwdHash)
        {
            var sql = "SELECT user_pwd_hash FROM default_schema.user where user_id=:userID and user_pwd_hash=:pwdHash";
            if (Db.QueryForFirstValue(sql, new[] { ("userID", (object)userId), ("pwdHash", (object)pwdHash) }) != null)
                return true;
            else return false;
        }
        public double GetVipLevelDiscount(long userId)
        {
            var sql = "SELECT vip_discount FROM default_schema.user,default_schema.vip where user_id=:userID and user_vip_level=vip_level";
            var discount = Db.QueryForFirstValue(sql, new[] { ("userID", (object)userId) });
            if (discount != null)
                return (double)discount;
            else return -1;
        }
        public bool UpdateUserPwdHash(long userId, string newValue)
        {
            var sql = "UPDATE default_schema.user SET user_pwd_hash=:newValue where user_id=:userId";
            var flag = Db.Query(sql, 1, new[] { ("newValue", (object)newValue), ("userId", (object)userId) });
            if (flag == 1) return true;
            else return false;
        }
        public bool UpdateUserVipLevel(long userId, long newValue)
        {
            var sql = "UPDATE default_schema.user SET user_vip_level=:newValue where user_id=:userId";
            var flag = Db.Query(sql, 1, new[] { ("newValue", (object)newValue), ("userId", (object)userId) });
            if (flag == 1) return true;
            else return false;
        }
        public bool UpdateUserVipLevelExpireTime(long userId, DateTime newValue)
        {
            var sql = "UPDATE default_schema.user SET user_vip_level_expire_time=:newValue WHERE user_id=:userId";
            var flag = Db.Query(sql, 1, new[] { ("newValue", (object)newValue), ("userId", (object)userId) });
            if (flag == 1) return true;
            else return false;
        }
        public IUserModel? GetUser(long userId)
        {
            var sql = "SELECT * FROM default_schema.user WHERE user_id=:userId";
            var result = Db.QueryForFirstRow(sql, new[] { ("userId", (object)userId) });
            if (result == null)
                return null;
            else
            {
                var model = new UserModel(
                (long)result["user_id"],
                (string)result["user_pwd_hash"],
                (long)result["user_vip_level"],
                (DateTime)result["user_vip_level_expire_time"]);
                return model;
            }
        }
    }
}
