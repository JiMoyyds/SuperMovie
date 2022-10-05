namespace SuperMovieSDK.Container.Schedule.Entity;

using Film.Entity;
using Cinema.Entity;

public interface IScheduleEntity
{
    public bool Drop();
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICinemaEntity Cinema { get; set; }
    public IFilmEntity Film { get; set; }
}