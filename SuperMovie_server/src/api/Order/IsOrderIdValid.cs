namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

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
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<IsOrderIdValidReq>(e.Data);
        var rsp = new IsOrderIdValidRsp
        {
            Ok = false
        };
    }
}