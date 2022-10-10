namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;

public struct GetAllOrderWithUserIdReq
{
    public long OrderUserId;
}

public struct GetAllOrderWithUserIdRsp
{
    public List<OrderRsp> Collection;
}

//api : get_all_order_with_user
public class GetAllOrderWithUserId : WebSocketBehavior
{
}