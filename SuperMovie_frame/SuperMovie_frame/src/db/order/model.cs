namespace SuperMovie.Db.Order.Model;

//resolver : LYF
//asm file : OrderDbImpl
//impl :: SuperMovie.Db.Order.Model.OrderModel
//impl proj struct :
//src/db/model.cs
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