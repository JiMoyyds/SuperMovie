using SuperMovie.Server.Api.Schedule;

namespace SuperMovie.Server.Api.Cinema;

using Container.Cinema.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllAvailableCinemaByFilmIdReq
{
    public long FilmId;
}

public struct GetAllAvailableCinemaByFilmIdRsp
{
    public List<CinemaRsp> Collection;
}

//api : get_all_available_cinema_by_film_id
public class GetAllAvailableCinemaByFilmId : WebSocketBehavior
{
    private ICinemaProvider _cinemaProvider;

    public void Set(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_available_cinema_by_film_id req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllAvailableCinemaByFilmIdReq>(e.Data);
        var cinemas = _cinemaProvider.GetAllCinema();

        var cinemaRspList = new List<CinemaRsp>();

        foreach (var cinema in cinemas)
        {
            if (cinema.Schedules.Any(s => s.Film.Id == req.FilmId))
            {
                var cinemaRsp = new CinemaRsp
                {
                    CinemaId = cinema.Id,
                    CinemaName = cinema.Name
                };
                cinemaRspList.Add(cinemaRsp);
            }
        }

        var rsp = new GetAllAvailableCinemaByFilmIdRsp
        {
            Collection = cinemaRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_available_cinema_by_film_id rsp:\n{json}");
        Send(json);
    }
}