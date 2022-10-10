namespace SuperMovie.Server.Api.User;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct UpgradeUserVipReq
{
    public long UserId;
    public long NewVipLevel;
}

public struct UpgradeUserVipRsp
{
    public bool Ok;
}

//api : upgrade_user_vip
public class UpgradeUserVip : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<UpgradeUserVipReq>(e.Data);
        var rsp = new UpgradeUserVipRsp
        {
            Ok = false
        };
       
        Send(JsonHelper.Stringify(rsp));
    }
}