namespace SuperMovie.Container.Schedule.Entity;

using Film.Entity;
using Cinema.Entity;

//resolver : WZJ
//asm file : ScheduleImpl
//impl :: SuperMovie.Container.Schedule.Entity.ScheduleEntity
//impl proj struct :
//src/container/entity.cs
public interface IScheduleEntity
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
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICinemaEntity? Cinema { get; set; }
    public IFilmEntity? Film { get; set; }
}