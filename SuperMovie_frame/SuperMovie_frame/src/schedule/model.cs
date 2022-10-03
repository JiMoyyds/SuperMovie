namespace SuperMovieSDK.Schedule.Model;

public interface IScheduleModel
{
    public long Id { get; set; }
    public long CinemaId { get; set; }
    public long FilmId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}