using SuperMovie.Container.Order.Provider;

namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct RefundOrderReq
{
    public long OrderId;
}

public struct RefundOrderRsp
{
    public bool Ok;
}

//api : refund_order
public class RefundOrder : WebSocketBehavior
{
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"refund_order req:\n{e.Data}");

        var req = JsonHelper.Parse<RefundOrderReq>(e.Data);

        var order = _orderProvider.GetOrder(req.OrderId);

        RefundOrderRsp rsp;

        if (order != null && order.Status == "paid")
        {
            //TODO AlipayF2F

            order.Status = "refunded";
            rsp = new RefundOrderRsp
            {
                Ok = true
            };
        }
        else
        {
            rsp = new RefundOrderRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"refund_order rsp:\n{json}");
        Send(json);
    }
}