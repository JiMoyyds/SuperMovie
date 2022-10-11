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
    public DateTime FilmOnlineTime;
    public List<string> FilmTypes;
    public List<string> FilmActors;
}

public struct UpdateFilmRsp
{
    public bool Ok;
}

//api : update_film
public class UpdateFilm : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"update_film req:\n{e.Data}");

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
            film.OnlineTime = req.FilmOnlineTime;

            film.ClearType();
            foreach (var type in req.FilmTypes)
                film.AddType(type);
            film.ClearActor();
            foreach (var actor in req.FilmActors)
                film.AddActor(actor);

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

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"update_film rsp:\n{json}");
        Send(json);
    }
}