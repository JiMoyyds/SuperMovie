namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

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
    public long OrderId;
}

//api : create_order
public class CreateOrder : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<CreateOrderReq>(e.Data);
        var rsp = new CreateOrderRsp
        {
            Ok = false,
            OrderId = 0
        };
    }
}