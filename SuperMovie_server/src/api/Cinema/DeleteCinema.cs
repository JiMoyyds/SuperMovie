namespace SuperMovie.Server.Api.Cinema;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}