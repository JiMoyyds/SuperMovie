namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}