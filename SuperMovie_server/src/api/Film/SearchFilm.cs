using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Schedule.Provider;

namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct SearchFilmReq
{
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
    private readonly IFilmProvider _filmProvider;
    private readonly IScheduleProvider _scheduleProvider;

    public SearchFilm(IFilmProvider filmProvider, IScheduleProvider scheduleProvider)
    {
        _filmProvider = filmProvider;
        _scheduleProvider = scheduleProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<SearchFilmReq>(e.Data);
        var filterByFilmName = _filmProvider
            .FilterFilmByName(req.FilmNameKeyWord);
        var filterByFilmTypes = _filmProvider
            .FilterFilmByTypes(req.FilmTypes);
        var filterByFilmOnlineTime = _filmProvider
            .FilterFilmByOnlineTime(req.FilmOnlineTimeStart, req.FilmOnlineTimeEnd);
        var filterByScheduleTime = () =>
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

        var filmEntities =
            new List<IFilmEntity>();
        filmEntities.AddRange(filterByFilmName);
        filmEntities.AddRange(filterByFilmTypes);
        filmEntities.AddRange(filterByFilmOnlineTime);
        filmEntities.AddRange(filterByScheduleTime());

        var filmRspList = new List<FilmRsp>();

        foreach (var film in filmEntities.DistinctBy(x => x.Id))
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

        var rsp = new SearchFilmRsp
        {
            Collection = filmRspList
        };

        Send(JsonHelper.Stringify(rsp));
    }
}