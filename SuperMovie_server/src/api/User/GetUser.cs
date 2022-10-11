namespace SuperMovie.Server.Api.User;

using Container.User.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetUserReq
{
    public long UserId;
}

public struct GetUserRsp
{
    public bool Ok;
    public long UserId;
    public long UserVipLevel;
    public double UserVipLevelDiscount;
    public DateTime UserVipExpireTime;
}

//api : get_user
public class GetUser : WebSocketBehavior
{
    private IUserProvider _userProvider;

    public void Set(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_user req:\n{e.Data}");

        var req = JsonHelper.Parse<GetUserReq>(e.Data);

        var user = _userProvider.GetUser(req.UserId);

        GetUserRsp rsp;
        if (user != null)
        {
            rsp = new GetUserRsp
            {
                Ok = true,
                UserId = user.Id,
                UserVipLevel = user.Vip.Level,
                UserVipLevelDiscount = user.Vip.Discount,
                UserVipExpireTime = user.VipLevelExpireTime.Value
            };
        }
        else
        {
            rsp = new GetUserRsp
            {
                Ok = false,
                UserId = -1,
                UserVipLevel = -1,
                UserVipLevelDiscount = -1,
                UserVipExpireTime = DateTime.Now
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_user rsp:\n{json}");
        Send(json);
    }
}