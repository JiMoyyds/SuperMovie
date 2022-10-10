using SuperMovie.Container.Order.Provider;

namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

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
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_order req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllOrderReq>(e.Data);
        var orders = _orderProvider.GetAllOrder();

        var orderRspList = new List<OrderRsp>();

        foreach (var order in orders)
        {
            var orderRsp = new OrderRsp
            {
                OrderId = order.Id,
                OrderUserId = order.User.Id,
                OrderFilmId = order.Film.Id,
                OrderCinemaId = order.Cinema.Id,
                OrderScheduleId = order.Schedule.Id,
                OrderSeat = order.Seat,
                OrderPayAmount = order.PayAmount
            };
            orderRspList.Add(orderRsp);
        }

        var rsp = new GetAllOrderRsp
        {
            Collection = orderRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_order rsp:\n{json}");
        Send(json);
    }
}