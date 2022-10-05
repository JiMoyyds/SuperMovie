namespace SuperMovieSDK.Container.Order.Provider;

using Entity;
using Film.Entity;
using User.Entity;
using Cinema.Entity;
using Schedule.Entity;

public interface IOrderProvider
{
    public IOrderEntity? CreateOrder
    (
        IUserEntity user,
        long payAmount,
        IFilmEntity film,
        ICinemaEntity cinema,
        IScheduleEntity schedule,
        string seat
    );

    public IOrderEntity? GetOrder(long id);
    public List<IOrderEntity> GetAllOrder();
}