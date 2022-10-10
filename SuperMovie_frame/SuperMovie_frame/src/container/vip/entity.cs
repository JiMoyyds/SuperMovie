namespace SuperMovie.Container.Vip.Entity;

//resolver : HZC
//asm file : VipContainerImpl
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

    public long Level { get; }
    public double Discount { get; set; }
    public double MonthPrice { get; set; }
}