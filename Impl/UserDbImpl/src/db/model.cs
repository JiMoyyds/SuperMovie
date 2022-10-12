namespace SuperMovie.Db.User.Model
{
    public class UserModel : IUserModel
    {
        public long Id { get; set; }
        public string PwdHash { get; set; }
        public long VipLevel { get; set; }
        public DateTime VipLevelExpireTime { get; set; }
        public UserModel(long id, string pwdhash, long viplevel, DateTime VipLevelExpireTime)
        {
            this.Id = id;
            this.PwdHash = pwdhash;
            this.VipLevel = viplevel;
            this.VipLevelExpireTime = VipLevelExpireTime;
        }
    }
}
