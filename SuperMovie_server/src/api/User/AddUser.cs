namespace SuperMovie_server.api.User;

using WebSocketSharp;
using WebSocketSharp.Server;

public struct AddUserReq
{
    public long UserId;
    public long UserPwd;
}

public struct AddUserRsp
{
    public bool Ok;
}

//api : add_user
public class AddUser : WebSocketBehavior
{
}