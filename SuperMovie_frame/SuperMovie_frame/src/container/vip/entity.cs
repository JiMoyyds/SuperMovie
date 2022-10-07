namespace SuperMovie.Container.Vip.Entity;

//resolver : HZC
//asm file : VipImpl
//impl :: SuperMovie.Container.Vip.Entity.VipEntity
//impl proj struct :
//src/container/entity.cs
public interface IVipEntity
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

    public long Level { get; set; }
    public long Discount { get; set; }
}