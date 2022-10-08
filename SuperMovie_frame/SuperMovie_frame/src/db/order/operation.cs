namespace SuperMovie.Db.Order.Operation;

using Model;

//resolver : LYF
//asm file : OrderDbImpl
//impl :: SuperMovie.Db.Order.Operation.OrderOperation
//impl proj struct :
//src/db/operation.cs
public interface IOrderOperation
{
    public bool CreateOrder(IOrderModel model);
    public bool DeleteOrder(long orderId);
    public IOrderModel? GetOrder(long orderId);
    public List<IOrderModel> GetAllOrder();
    public List<IOrderModel> FilterOrderByUserId(long userId);
    public List<IOrderModel> FilterOrderByFilmId(long filmId);
    public List<IOrderModel> FilterOrderByFilmTypes(List<string> filmTypes);
    public List<IOrderModel> FilterOrderByFilmActors(List<string> filmActors);
    public bool IsOrderIdValid(long orderId);
    public bool UpdateOrderUserId(long orderId, long newValue);
    public bool UpdateOrderPayAmount(long orderId, double newValue);
    public bool UpdateOrderTime(long orderId, DateTime newValue);
    public bool UpdateOrderFilmId(long orderId, long newValue);
    public bool UpdateOrderCinemaId(long orderId, long newValue);
    public bool UpdateOrderScheduleId(long orderId, long newValue);
    public bool UpdateOrderCinemaSeat(long orderId, string newValue);
}