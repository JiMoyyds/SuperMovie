namespace SuperMovie.Server.Api.Order;

public struct CreateOrderReq
{
    public long OrderFilmId;
    public long OrderCinemaId;
    public long OrderScheduleId;
    public long OrderSeat;
    public double OrderPayAmount;
}

//api : create_order
public class CreateOrder
{
}