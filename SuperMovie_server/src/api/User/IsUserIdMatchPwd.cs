namespace SuperMovie_server.api.User;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}