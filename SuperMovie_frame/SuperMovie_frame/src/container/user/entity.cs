namespace SuperMovieSDK.Container.User.Entity;

using Vip.Entity;
using Order.Entity;

public interface IUserEntity
{
    public bool Drop();

    public long Id { get; set; }
    public bool ResetPwd(string oldPwd, string newPwd);
    
    public List<IOrderEntity> Orders { get; }
    public bool AddOrder(IOrderEntity order);
    public bool RemoveOrder(IOrderEntity order);
    public bool ClearOrder();
    
    public IVipEntity Vip { get; set; }
    public DateTime VipLevelExpireTime { get; set; }
}