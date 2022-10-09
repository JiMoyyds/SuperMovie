namespace SuperMovie.Container.Order.Provider;

using Entity;
using Film.Entity;
using User.Entity;
using Cinema.Entity;
using Schedule.Entity;

//resolver : LYF
//asm file : OrderContainerImpl
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
    public List<IOrderEntity> FilterOrderByUserId(long userId);
    public List<IOrderEntity> FilterOrderByFilmId(long filmId);
    public List<IOrderEntity> FilterOrderByFilmTypes(List<string> filmTypes);
    public List<IOrderEntity> FilterOrderByFilmActors(List<string> filmActors);
}