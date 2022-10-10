namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

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
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<GetAllOrderWithUserIdReq>(e.Data);
        var rsp = new GetAllOrderWithUserIdRsp
        {
            Collection = new List<OrderRsp>()
        };
    }
}