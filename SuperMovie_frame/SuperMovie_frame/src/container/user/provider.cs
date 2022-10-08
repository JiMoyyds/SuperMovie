namespace SuperMovie.Container.User.Provider;

using Entity;
using Vip.Entity;

//resolver : HZC
//asm file : UserContainerImpl
//impl :: SuperMovie.Container.User.Provider.UserProvider
//impl proj struct :
//src/container/provider.cs
public interface IUserProvider
{
    public IUserEntity? CreateUser
    (
        long id,
        string pwd,
        IVipEntity vip,
        DateTime vipLevelExpireTime
    );

    public IUserEntity? GetUser(long id);
    public IUserEntity? IsIdMatchPwd(long id, string pwd);
    public List<IUserEntity> GetAllUser();
}