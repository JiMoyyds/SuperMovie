namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct UpdateFilmReq
{
    public long FilmId;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct UpdateFilmRsp
{
    public bool Ok;
}

//api : update_film
public class UpdateFilm : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<UpdateFilmReq>(e.Data);
        var rsp = new UpdateFilmRsp
        {
            Ok = false
        };
    }
}