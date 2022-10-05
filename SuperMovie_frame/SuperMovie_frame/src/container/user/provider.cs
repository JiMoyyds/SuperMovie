namespace SuperMovieSDK.Container.User.Provider;

using Entity;
using Vip.Entity;

public interface IUserProvider
{
    public IUserEntity? CreateUser
    (
        long id,
        string pwd,
        IVipEntity vip,
        DateTime vipLevelExpireTime
    );

    public IUserEntity? GetUser(long id, string pwd);
    public List<IUserEntity> GetAllUser();
}