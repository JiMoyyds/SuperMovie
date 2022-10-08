namespace SuperMovie.Db.User.Model;

//resolver : HZC
//asm file : UserDbImpl
//impl :: SuperMovie.Db.User.Model.UserModel
//impl proj struct :
//src/db/model.cs
public interface IUserModel
{
    public long Id { get; set; }
    public string PwdHash { get; set; }
    public long VipLevel { get; set; }
    public DateTime VipLevelExpireTime { get; set; }
}