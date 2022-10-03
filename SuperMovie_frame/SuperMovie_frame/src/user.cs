namespace SuperMovieSDK;

public interface IUserModel
{
    public long Id { get; set; }
    public string PwdHash { get; set; }
    public long VipLevel { get; set; }
    public DateTime VipLevelExpireTime { get; set; }
}

public interface IUserOperation
{
    public bool CreateUser(IUserModel model);
    public bool DeleteUser(long userId);
    public List<IUserModel> GetAllUser();
    public bool IsPwdHashValid(long userId, string pwdHash);
    public double GetVipLevelDiscount(long userId);
    public bool UpdateUserPwdHash(long userId, string newValue);
    public bool UpdateUserVipLevel(long userId, long newValue);
    public bool UpdateUserVipLevelExpireTime(long userId, DateTime newValue);
}