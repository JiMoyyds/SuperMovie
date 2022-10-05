namespace SuperMovieSDK.Db.User.Operation;

using Model;

//由hzc实现
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