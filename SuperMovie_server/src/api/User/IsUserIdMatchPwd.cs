namespace SuperMovie.Server.Api.User;

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
    public bool Ok;
}

//api : is_user_id_match_pwd
public class IsUserIdMatchPwd : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<IsUserIdMatchPwdReq>(e.Data);
        var rsp = new IsUserIdMatchPwdRsp
        {
            Ok = false
        };
    }
}