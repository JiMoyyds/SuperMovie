namespace SuperMovie.Server.Api.User;

using Container.User.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct IsUserIdMatchPwdReq
{
    public long UserId;
    public string UserPwd;
}

public struct IsUserIdMatchPwdRsp
{
    public bool Yes;
}

//api : is_user_id_match_pwd
public class IsUserIdMatchPwd : WebSocketBehavior
{
    private IUserProvider _userProvider;

    public void Set(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"is_user_id_match_pwd req:\n{e.Data}");

        var req = JsonHelper.Parse<IsUserIdMatchPwdReq>(e.Data);

        var user = _userProvider.IsIdMatchPwd(req.UserId, req.UserPwd);

        var rsp = new IsUserIdMatchPwdRsp
        {
            Yes = user != null
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"is_user_id_match_pwd rsp:\n{json}");
        Send(json);
    }
}