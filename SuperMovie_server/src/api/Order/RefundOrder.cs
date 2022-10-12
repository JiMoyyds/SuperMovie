using AlipayF2F;
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
    private F2FClient _f2fClient;

    public void Set(IOrderProvider orderProvider, F2FClient f2fClient)
    {
        _orderProvider = orderProvider;
        _f2fClient = f2fClient;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"refund_order req:\n{e.Data}");

        var req = JsonHelper.Parse<RefundOrderReq>(e.Data);

        var order = _orderProvider.GetOrder(req.OrderId);

        RefundOrderRsp rsp;

        if (order != null && order.Schedule.StartTime > DateTime.Now && order.Status == "paid")
        {
            //TODO AlipayF2F
            var f2fReq = F2FRequest.RefundTrade(order.Id.ToString(), order.PayAmount);
            var f2fRsp = _f2fClient.ExecuteRequest(f2fReq);
            Console.WriteLine(f2fRsp.Msg);

            var f2fReq2 = F2FRequest.QueryTrade(order.Id.ToString());
            var f2fRsp2 = _f2fClient.ExecuteRequest(f2fReq2);
            if (F2FResponse.IsTradeClosed(f2fRsp2))
            {
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