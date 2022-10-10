namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllFilmReq
{
}

public struct FilmRsp
{
    public long FilmId;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct GetAllFilmRsp
{
    public List<FilmRsp> Collection;
}

//api : get_all_film
public class GetAllFilm : WebSocketBehavior
{
    private readonly IFilmProvider _filmProvider;

    public GetAllFilm(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<GetAllFilmReq>(e.Data);
        var films = _filmProvider.GetAllFilm();

        var filmRspList = new List<FilmRsp>();

        foreach (var film in films)
        {
            var filmRsp = new FilmRsp
            {
                FilmId = film.Id,
                FilmName = film.Name,
                FilmCoverUrl = film.CoverUrl,
                FilmPreviewVideoUrl = film.PreviewVideoUrl,
                FilmPrice = film.Price,
                FilmIsPreorder = film.IsPreorder
            };
            filmRspList.Add(filmRsp);
        }

        var rsp = new GetAllFilmRsp
        {
            Collection = new List<FilmRsp>()
        };
    }
}