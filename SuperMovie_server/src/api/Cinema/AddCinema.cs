namespace SuperMovie.Server.Api.Cinema;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddCinemaReq
{
    public string CinemaName;
}

public struct AddCinemaRsp
{
    public bool Ok;
    public long CinemaId;
}

//api : add_cinema
public class AddCinema : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddCinemaReq>(e.Data);
        var rsp = new AddCinemaRsp
        {
            Ok = false,
            CinemaId = 0
        };
    }
}