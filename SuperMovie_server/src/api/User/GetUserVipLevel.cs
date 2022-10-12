using SuperMovie.Container.Vip.Provider;

namespace SuperMovie.Server.Api.User;

using Container.User.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetUserVipLevelReq
{
    public long UserId;
}

public struct GetUserVipLevelRsp
{
    public bool Ok;
    public long UserId;
    public long UserVipLevel;
}

//api : get_user_vip_level
public class GetUserVipLevel : WebSocketBehavior
{
    private IUserProvider _userProvider;
    private IVipProvider _vipProvider;

    public void Set(IUserProvider userProvider, IVipProvider vipProvider)
    {
        _userProvider = userProvider;
        _vipProvider = vipProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_user_vip_level req:\n{e.Data}");

        var req = JsonHelper.Parse<GetUserVipLevelReq>(e.Data);

        var user = _userProvider.GetUser(req.UserId);

        GetUserVipLevelRsp rsp;

        if (user != null)
        {
            var orders = user.Orders;
            var count = 0.0;
            foreach (var order in orders)
            {
                if (order.Time.Value.Year == DateTime.Now.Year && order.Status == "paid")
                    count += order.PayAmount;
            }

            var vips = _vipProvider.GetAllVip();
            long max = 0;
            foreach (var vip in vips)
            {
                if (vip.MonthPrice > count && vip.Level > max)
                {
                    max = vip.Level;
                }
            }

            user.Vip = _vipProvider.GetVip(max);

            rsp = new GetUserVipLevelRsp
            {
                Ok = true,
                UserId = req.UserId,
                UserVipLevel = max
            };
        }
        else
        {
            rsp = new GetUserVipLevelRsp
            {
                Ok = false,
                UserId = req.UserId,
                UserVipLevel = 0
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_user_vip_level rsp:\n{json}");
        Send(json);
    }
}