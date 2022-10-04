namespace SuperMovieSDK.Order.Model;

//由lyf实现
public interface IOrderModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public double PayAmount { get; set; }
    public DateTime Time { get; set; }
    public long FilmId { get; set; }
    public long CinemaId { get; set; }
    public long ScheduleId { get; set; }
    public string CinemaSeat { get; set; }
}