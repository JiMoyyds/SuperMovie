namespace SuperMovie.Server.Api.User;

using SuperMovie.Container.User.Provider;
using SuperMovie.Container.Vip.Provider;
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
    private IUserProvider _userProvider;
    private IVipProvider _vipProvider;

    public void Set(
        IUserProvider userProvider,
        IVipProvider vipProvider
    )
    {
        _userProvider = userProvider;
        _vipProvider = vipProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"upgrade_user_vip req:\n{e.Data}");

        var req = JsonHelper.Parse<UpgradeUserVipReq>(e.Data);
        var user = _userProvider.GetUser(req.UserId);

        UpgradeUserVipRsp rsp;

        if (user != null)
        {
            var vip = _vipProvider.GetVip(req.NewVipLevel);

            if (vip != null)
                user.Vip = vip;

            rsp = new UpgradeUserVipRsp
            {
                Ok = user.Vip.Level == vip.Level
            };
        }
        else
        {
            rsp = new UpgradeUserVipRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"upgrade_user_vip rsp:\n{json}");
        Send(json);
    }
}