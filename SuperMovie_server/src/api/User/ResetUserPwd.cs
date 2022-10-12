namespace SuperMovie.Server.Api.User;

using Container.User.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct ResetUserPwdReq
{
    public long UserId;
    public string UserOldPwd;
    public string UserNewPwd;
}

public struct ResetUserPwdRsp
{
    public bool Yes;
}

//api : reset_user_pwd
public class ResetUserPwd : WebSocketBehavior
{
    private IUserProvider _userProvider;

    public void Set(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"reset_user_pwd req:\n{e.Data}");

        var req = JsonHelper.Parse<ResetUserPwdReq>(e.Data);

        var user = _userProvider.GetUser(req.UserId);
        ResetUserPwdRsp rsp;
        if (user != null)
        {
            var ok = user.ResetPwd(req.UserOldPwd, req.UserNewPwd);

            rsp = new ResetUserPwdRsp
            {
                Yes = ok
            };
        }
        else
        {
            rsp = new ResetUserPwdRsp
            {
                Yes = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"reset_user_pwd rsp:\n{json}");
        Send(json);
    }
}