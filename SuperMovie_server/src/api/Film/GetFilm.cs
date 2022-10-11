namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetFilmReq
{
    public long FilmId;
}

public struct GetFilmRsp
{
    public bool Ok;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
    public DateTime FilmOnlineTime;
    public List<string> FilmTypes;
    public List<string> FilmActors;
}

//api : get_film
public class GetFilm : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_film req:\n{e.Data}");

        var req = JsonHelper.Parse<GetFilmReq>(e.Data);
        var film = _filmProvider.GetFilm(req.FilmId);

        GetFilmRsp rsp;

        if (film != null)
        {
            rsp = new GetFilmRsp
            {
                Ok = true,
                FilmName = film.Name,
                FilmCoverUrl = film.CoverUrl,
                FilmPreviewVideoUrl = film.PreviewVideoUrl,
                FilmPrice = film.Price,
                FilmIsPreorder = film.IsPreorder,
                FilmOnlineTime = film.OnlineTime.Value,
                FilmTypes = film.Types,
                FilmActors = film.Actors
            };
        }
        else
        {
            rsp = new GetFilmRsp
            {
                Ok = false,
                FilmName = "",
                FilmCoverUrl = "",
                FilmPreviewVideoUrl = "",
                FilmPrice = -1,
                FilmIsPreorder = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_film rsp:\n{json}");
        Send(json);
    }
}