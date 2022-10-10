using SuperMovie.Server.Api.Cinema;

namespace SuperMovie.Server.Api.Film;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddFilmReq
{
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct AddFilmRsp
{
    public bool Ok;
    public long FilmId;
}

//api : add_film
public class AddFilm : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddFilmReq>(e.Data);
        var rsp = new AddFilmRsp
        {
            Ok = false,
            FilmId = 0
        };
    }
}