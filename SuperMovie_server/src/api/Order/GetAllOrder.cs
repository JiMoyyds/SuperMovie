namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;

public struct GetAllOrderReq
{
}

public struct OrderRsp
{
    public long OrderId;
    public long OrderUserId;
    public long OrderFilmId;
    public long OrderCinemaId;
    public long OrderScheduleId;
    public string OrderSeat;
    public double OrderPayAmount;
}

public struct GetAllOrderRsp
{
    public List<OrderRsp> Collection;
}

//api : get_all_order
public class GetAllOrder : WebSocketBehavior
{
}