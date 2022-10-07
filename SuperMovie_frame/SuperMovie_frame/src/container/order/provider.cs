namespace SuperMovie.Container.Order.Provider;

using Entity;
using Film.Entity;
using User.Entity;
using Cinema.Entity;
using Schedule.Entity;

//resolver : LYF
//asm file : OrderImpl
//impl :: SuperMovie.Container.Order.Provider.OrderProvider
//impl proj struct :
//src/container/provider.cs
public interface IOrderProvider
{
    public IOrderEntity? CreateOrder
    (
        IUserEntity user,
        double payAmount,
        IFilmEntity film,
        ICinemaEntity cinema,
        IScheduleEntity schedule,
        string seat
    );

    public IOrderEntity? GetOrder(long id);
    public List<IOrderEntity> GetAllOrder();
}