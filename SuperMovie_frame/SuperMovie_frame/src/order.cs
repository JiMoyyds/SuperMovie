namespace SuperMovieSDK;

public interface Order
{
    public long OrderId { get; set; }
    public long UserId { get; set; }
    public long PayAmount { get; set; }
    public DateTime OrderTime { get; set; }
    public long OrderFilmId { get; set; }
}