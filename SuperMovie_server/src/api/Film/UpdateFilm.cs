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
    private readonly IFilmProvider _filmProvider;

    public UpdateFilm(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<UpdateFilmReq>(e.Data);
        var film = _filmProvider.GetFilm(req.FilmId);

        UpdateFilmRsp rsp;

        if (film != null)
        {
            film.Name = req.FilmName;
            film.CoverUrl = req.FilmCoverUrl;
            film.PreviewVideoUrl = req.FilmPreviewVideoUrl;
            film.Price = req.FilmPrice;
            film.IsPreorder = req.FilmIsPreorder;

            rsp = new UpdateFilmRsp
            {
                Ok = true
            };
        }
        else
        {
            rsp = new UpdateFilmRsp
            {
                Ok = false
            };
        }

        Send(JsonHelper.Stringify(rsp));
    }
}