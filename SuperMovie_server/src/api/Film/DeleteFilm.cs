namespace SuperMovie.Server.Api.Film;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct DeleteFilmReq
{
    public long FilmId;
}

public struct DeleteFilmRsp
{
    public bool Ok;
}

//api : delete_film
public class DeleteFilm : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<DeleteFilmReq>(e.Data);
        var 
    }
}