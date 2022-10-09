namespace SuperMovie.Server.Api.Order;

public struct CreateOrderReq
{
    public long OrderFilmId;
    public long OrderUserId;
    public long OrderCinemaId;
    public long OrderScheduleId;
    public long OrderSeat;
    public double OrderPayAmount;
}

public struct CreateOrderRsp
{
    public bool Ok;
    public bool OrderId;
}

//api : create_order
public class CreateOrder
{
}