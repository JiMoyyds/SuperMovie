using SuperMovie.Server.Api.Cinema;

namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddFilmReq
{
    public string FilmName;
    public DateTime FilmOnlineTime;
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
    private readonly IFilmProvider _filmProvider;

    public AddFilm(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddFilmReq>(e.Data);

        var film = _filmProvider.CreateFilm
        (
            req.FilmName,
            req.FilmOnlineTime,
            req.FilmIsPreorder,
            req.FilmPrice
        );

        AddFilmRsp rsp;

        if (film != null)
        {
            rsp = new AddFilmRsp
            {
                Ok = true,
                FilmId = film.Id
            };
        }
        else
        {
            rsp = new AddFilmRsp
            {
                Ok = false,
                FilmId = 0
            };
        }

        Send(JsonHelper.Stringify(rsp));
    }
}