namespace SuperMovieSDK.Container.Cinema.Entity;

using Schedule.Entity;

public interface ICinemaEntity
{
    public bool Drop();

    public string Name { get; set; }

    public List<IScheduleEntity> Schedules { get; }
    public bool AddSchedule(IScheduleEntity schedule);
    public bool RemoveSchedule(IScheduleEntity schedule);
    public bool ClearSchedule();
}