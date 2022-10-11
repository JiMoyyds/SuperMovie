using SuperMovie.Server.Api.Cinema;

namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
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
    public DateTime FilmOnlineTime;
    public List<string> FilmTypes;
    public List<string> FilmActors;
}

public struct AddFilmRsp
{
    public bool Ok;
    public long FilmId;
}

//api : add_film
public class AddFilm : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"add_film req:\n{e.Data}");

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
            film.CoverUrl = req.FilmCoverUrl;
            film.PreviewVideoUrl = req.FilmPreviewVideoUrl;

            foreach (var type in req.FilmTypes)
                film.AddType(type);
            foreach (var actor in req.FilmActors)
                film.AddActor(actor);

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

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"add_film rsp:\n{json}");
        Send(json);
    }
}