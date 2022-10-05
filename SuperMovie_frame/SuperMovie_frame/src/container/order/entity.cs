namespace SuperMovieSDK.Container.Order.Entity;

using User.Entity;
using Film.Entity;
using Cinema.Entity;
using Schedule.Entity;

public interface IOrderEntity
{
    public bool Drop();
    
    public IUserEntity User { get; set; }
    public long PayAmount { get; set; }
    public IFilmEntity Film { get; set; }
    public ICinemaEntity Cinema { get; set; }
    public IScheduleEntity Schedule { get; set; }
    public string Seat { get; set; }
    public DateTime Time { get; set; }
}