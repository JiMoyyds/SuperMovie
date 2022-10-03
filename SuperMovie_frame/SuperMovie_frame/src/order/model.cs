namespace SuperMovieSDK.Order.Model;

public interface IOrderModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PayAmount { get; set; }
    public DateTime Time { get; set; }
    public long FilmId { get; set; }
    public long CinemaId { get; set; }
    public long ScheduleId { get; set; }
    public long CinemaSeat { get; set; }
}