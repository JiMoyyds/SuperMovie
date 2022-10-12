using SuperMovie.Container.Order.Provider;
using AlipayF2F;

namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetOrderReq
{
    public long OrderId;
}

public struct GetOrderRsp
{
    public bool Ok;
    public long OrderId;
    public long OrderUserId;
    public long OrderFilmId;
    public long OrderCinemaId;
    public long OrderScheduleId;
    public DateTime OrderTime;
    public string OrderSeat;
    public double OrderPayAmount;
    public string OrderStatus;
}

//api : get_order
public class GetOrder : WebSocketBehavior
{
    private IOrderProvider _orderProvider;
    private F2FClient _f2fClient;

    public void Set(IOrderProvider orderProvider, F2FClient f2fClient)
    {
        _orderProvider = orderProvider;
        _f2fClient = f2fClient;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_order req:\n{e.Data}");

        var req = JsonHelper.Parse<GetOrderReq>(e.Data);
        var order = _orderProvider.GetOrder(req.OrderId);

        GetOrderRsp rsp;

        if (order != null)
        {
            if (order.Status == "created")
            {
                var f2fReq = F2FRequest.QueryTrade(order.Id.ToString());
                var f2fRsp = _f2fClient.ExecuteRequest(f2fReq);
                var isSuccess = F2FResponse.IsTradeSuccess(f2fRsp);
                if (isSuccess)
                {
                    order.Status = "paid";
                }
            }

            rsp = new GetOrderRsp
            {
                Ok = true,
                OrderId = order.Id,
                OrderUserId = order.User.Id,
                OrderFilmId = order.Film.Id,
                OrderCinemaId = order.Cinema.Id,
                OrderScheduleId = order.Schedule.Id,
                OrderSeat = order.Seat,
                OrderPayAmount = order.PayAmount,
                OrderStatus = order.Status,
                OrderTime = order.Time.Value
            };
        }
        else
        {
            rsp = new GetOrderRsp
            {
                Ok = false,
                OrderId = 0,
                OrderUserId = 0,
                OrderFilmId = 0,
                OrderCinemaId = 0,
                OrderScheduleId = 0,
                OrderSeat = "",
                OrderPayAmount = 0.0,
                OrderStatus = ""
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_order rsp:\n{json}");
        Send(json);
    }
}