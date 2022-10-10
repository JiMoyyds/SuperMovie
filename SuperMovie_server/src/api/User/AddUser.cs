namespace SuperMovie.Server.Api.User;

using SuperMovie.Container.User.Provider;
using SuperMovie.Container.Vip.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddUserReq
{
    public long UserId;
    public string UserPwd;
}

public struct AddUserRsp
{
    public bool Ok;
}

//api : add_user
public class AddUser : WebSocketBehavior
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
        Console.WriteLine($"add_user req:\n{e.Data}");

        var req = JsonHelper.Parse<AddUserReq>(e.Data);
        var vipLevel0 = _vipProvider.GetVip(0);

        AddUserRsp rsp;

        if (vipLevel0 != null)
        {
            var user = _userProvider.CreateUser(req.UserId, req.UserPwd, vipLevel0, DateTime.Now);

            if (user != null)
            {
                rsp = new AddUserRsp
                {
                    Ok = true
                };
            }
            else
            {
                rsp = new AddUserRsp
                {
                    Ok = false
                };
            }
        }
        else
        {
            rsp = new AddUserRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"add_user rsp:\n{json}");
        Send(json);
    }
}