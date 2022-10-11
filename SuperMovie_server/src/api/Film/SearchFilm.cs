using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Schedule.Provider;

namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct SearchFilmReq
{
    public bool EnableScheduleSearch;
    public List<string> FilmTypes;
    public DateTime FilmOnlineTimeStart;
    public DateTime FilmOnlineTimeEnd;
    public DateTime FilmScheduleTimeStart;
    public DateTime FilmScheduleTimeEnd;
    public string FilmNameKeyWord;
}

public struct SearchFilmRsp
{
    public List<FilmRsp> Collection;
}

//api : search_film
public class SearchFilm : WebSocketBehavior
{
    private IFilmProvider _filmProvider;
    private IScheduleProvider _scheduleProvider;

    public void Set(IFilmProvider filmProvider, IScheduleProvider scheduleProvider)
    {
        _filmProvider = filmProvider;
        _scheduleProvider = scheduleProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"search_film req:\n{e.Data}");

        var req = JsonHelper.Parse<SearchFilmReq>(e.Data);
        var filterByFilmName = _filmProvider
            .MatchFilmByName(req.FilmNameKeyWord);
        var filterByFilmTypes = _filmProvider
            .FilterFilmByTypes(req.FilmTypes);
        var filterByFilmOnlineTime = _filmProvider
            .FilterFilmByOnlineTime(req.FilmOnlineTimeStart, req.FilmOnlineTimeEnd);
        var filterByScheduleTimeF = () =>
        {
            var schedules = _scheduleProvider
                .FilterScheduleByTimespan(req.FilmScheduleTimeStart, req.FilmScheduleTimeEnd);
            var films = new List<IFilmEntity>();

            foreach (var schedule in schedules)
            {
                if (schedule.Film != null)
                    films.Add(schedule.Film);
            }

            return films;
        };
        var filterByScheduleTime = filterByScheduleTimeF();

        var filmsUnion = new List<IFilmEntity>();
        filmsUnion.AddRange(filterByFilmName);
        filmsUnion.AddRange(filterByFilmTypes);
        filmsUnion.AddRange(filterByFilmOnlineTime);
        if (req.EnableScheduleSearch)
            filmsUnion.AddRange(filterByScheduleTime);
        Console.WriteLine(JsonHelper.Stringify(filmsUnion));
        var filmsIntersection = new List<IFilmEntity>();
        foreach (var film in filmsUnion.DistinctBy(x => x.Id))
        {
            if (filterByFilmName.Exists(x => x.Id == film.Id) &&
                filterByFilmTypes.Exists(x => x.Id == film.Id) &&
                filterByFilmOnlineTime.Exists(x => x.Id == film.Id)
               )
            {
                if (req.EnableScheduleSearch)
                {
                    if (filterByScheduleTime.Exists(x => x.Id == film.Id))
                        filmsIntersection.Add(film);
                }
                else
                {
                    filmsIntersection.Add(film);
                }
            }
        }

        var filmRspList = new List<FilmRsp>();

        foreach (var film in filmsIntersection.DistinctBy(x => x.Id))
        {
            var filmRsp = new FilmRsp
            {
                FilmId = film.Id,
                FilmName = film.Name,
                FilmCoverUrl = film.CoverUrl,
                FilmPreviewVideoUrl = film.PreviewVideoUrl,
                FilmPrice = film.Price,
                FilmIsPreorder = film.IsPreorder,
                FilmOnlineTime = film.OnlineTime.Value,
                FilmTypes = film.Types,
                FilmActors = film.Actors
            };

            filmRspList.Add(filmRsp);
        }

        var rsp = new SearchFilmRsp
        {
            Collection = filmRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"search_film rsp:\n{json}");
        Send(json);
    }
}