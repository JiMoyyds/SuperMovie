namespace SuperMovieSDK.User.Model;

public interface IUserModel
{
    public long Id { get; set; }
    public string PwdHash { get; set; }
    public long VipLevel { get; set; }
    public DateTime VipLevelExpireTime { get; set; }
}