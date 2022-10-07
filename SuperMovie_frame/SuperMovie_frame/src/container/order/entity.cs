namespace SuperMovie.Container.Order.Entity;

using User.Entity;
using Film.Entity;
using Cinema.Entity;
using Schedule.Entity;

//resolver : LYF
//asm file : OrderImpl
//impl :: SuperMovie.Container.Order.Entity.OrderEntity
//impl proj struct :
//src/container/entity.cs
public interface IOrderEntity
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
    public IUserEntity? User { get; set; }
    public double PayAmount { get; set; }
    public IFilmEntity? Film { get; set; }
    public ICinemaEntity? Cinema { get; set; }
    public IScheduleEntity? Schedule { get; set; }
    public string Seat { get; set; }
    public DateTime Time { get; set; }
}