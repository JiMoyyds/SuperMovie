namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;

public struct IsOrderIdValidReq
{
    public long OrderId;
}

public struct IsOrderIdValidRsp
{
    public bool Ok;
}

//api : is_order_id_valid
public class IsOrderIdValid : WebSocketBehavior
{
}