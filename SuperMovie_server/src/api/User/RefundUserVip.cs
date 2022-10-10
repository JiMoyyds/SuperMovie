namespace SuperMovie.Server.Api.User;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct RefundUserVipReq
{
    public long UserId;
}

public struct RefundUserVipRsp
{
    public bool Ok;
}

//api : refund_user_vip
public class RefundUserVip : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<RefundUserVipReq>(e.Data);
    }
}