namespace SuperMovie.Server.Api.Cinema;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}