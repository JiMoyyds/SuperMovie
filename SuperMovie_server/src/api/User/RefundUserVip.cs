namespace SuperMovie_server.api.User;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}