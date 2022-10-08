namespace SuperMovie.Container.Cinema.Entity;

using Schedule.Entity;

//resolver : WZJ
//asm file : CinemaImpl
//impl :: SuperMovie.Container.Cinema.Entity.CinemaEntity
//impl proj struct :
//src/container/entity.cs
public interface ICinemaEntity
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
    public string? Name { get; set; }

    public List<IScheduleEntity> Schedules { get; }
    public bool AddSchedule(IScheduleEntity schedule);
    public bool RemoveSchedule(IScheduleEntity schedule);
    public bool ClearSchedule();
}