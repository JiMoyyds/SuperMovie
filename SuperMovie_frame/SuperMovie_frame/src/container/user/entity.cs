namespace SuperMovie.Container.User.Entity;

using Vip.Entity;
using Order.Entity;

//resolver : HZC
//asm file : UserContainerImpl
//impl :: SuperMovie.Container.User.Entity.UserEntity
//impl proj struct :
//src/container/entity.cs
public interface IUserEntity
{
    /// <summary>
    /// 释放实体
    /// </summary>
    /// <returns></returns>
    public bool Drop();

    /// <summary>
    /// 实体是否合法
    /// </summary>
    /// <returns></returns>
    public bool IsValid();

    public long Id { get; }
    public bool ResetPwd(string oldPwd, string newPwd);

    public List<IOrderEntity> Orders { get; }
    public bool ClearOrder();

    public IVipEntity? Vip { get; set; }
    public DateTime? VipLevelExpireTime { get; set; }
    public double GetIfRefundVipThenAmount();
}