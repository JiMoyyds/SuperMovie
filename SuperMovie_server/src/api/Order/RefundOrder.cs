namespace SuperMovie.Server.Api.Order;

public struct RefundOrderReq
{
    public long OrderId;
}

public struct RefundOrderRsp
{
    public bool Ok;
}

//api : refund_order
public class RefundOrder
{
}