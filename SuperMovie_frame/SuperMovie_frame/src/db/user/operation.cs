namespace SuperMovie.Db.User.Operation;

using Model;

//resolver : HZC
//asm file : UserDbImpl
//impl :: SuperMovie.Db.User.Operation.UserOperation
//impl proj struct :
//src/db/operation.cs
public interface IUserOperation
{
    public bool CreateUser(IUserModel model);
    public bool DeleteUser(long userId);
    public IUserModel? GetUser(long userId);
    public List<IUserModel> GetAllUser();
    public bool IsPwdHashValid(long userId, string pwdHash);
    public double GetVipLevelDiscount(long userId);
    public bool UpdateUserPwdHash(long userId, string newValue);
    public bool UpdateUserVipLevel(long userId, long newValue);
    public bool UpdateUserVipLevelExpireTime(long userId, DateTime newValue);
}