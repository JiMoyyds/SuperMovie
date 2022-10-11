namespace SuperMovie.Server.Api.Order;

using SuperMovie.Container.Order.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct IsOrderCanCheckInReq
{
    public long OrderId;
}

public struct IsOrderCanCheckInRsp
{
    public bool Yes;
}

//api : is_order_can_check_in
public class IsOrderCanCheckIn : WebSocketBehavior
{
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"is_order_can_check_in req:\n{e.Data}");

        var req = JsonHelper.Parse<IsOrderCanCheckInReq>(e.Data);
        var order = _orderProvider.GetOrder(req.OrderId);

        IsOrderCanCheckInRsp rsp;

        if (order != null && order.Status == "paid")
        {
            var schedule = order.Schedule;
            var now = DateTime.Now;
            rsp = new IsOrderCanCheckInRsp
            {
                Yes = schedule != null &&
                      schedule.StartTime < now &&
                      schedule.EndTime > now
            };
        }
        else
        {
            rsp = new IsOrderCanCheckInRsp
            {
                Yes = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"is_order_can_check_in rsp:\n{json}");
        Send(json);
    }
}