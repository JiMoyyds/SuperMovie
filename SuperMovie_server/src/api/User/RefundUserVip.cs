using SuperMovie.Container.User.Provider;
using SuperMovie.Server.Api.Cinema;

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
    private IUserProvider _userProvider;

    public void Set(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"refund_user_vip req:\n{e.Data}");

        var req = JsonHelper.Parse<RefundUserVipReq>(e.Data);

        var user = _userProvider.GetUser(req.UserId);

        RefundUserVipRsp rsp;

        if (user != null)
        {
            var amount = user.GetIfRefundVipThenAmount();

            //TODO 
            rsp = new RefundUserVipRsp
            {
                Ok = true
            };
        }
        else
        {
            rsp = new RefundUserVipRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"refund_user_vip rsp:\n{json}");
        Send(json);
    }
}