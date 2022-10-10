namespace SuperMovie.Server.Api.User;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

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
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddUserReq>(e.Data);
    }
}