namespace SuperMovie.Server.Api.Cinema;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct DeleteCinemaReq
{
    public long CinemaId;
}

public struct DeleteCinemaRsp
{
    public bool Ok;
}

//api : deleta_cinema
public class DeleteCinema : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddCinemaReq>(e.Data);
        var rsp = new DeleteCinemaRsp
        {
            Ok = false,
        };
    }
}