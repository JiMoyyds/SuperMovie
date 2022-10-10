using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Schedule.Provider;

namespace SuperMovie.Server.Api.Schedule;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddScheduleReq
{
    public long ScheduleFilmId;
    public long ScheduleCinemaId;
    public DateTime ScheduleStartTime;
    public DateTime ScheduleEndTime;
}

public struct AddScheduleRsp
{
    public bool Ok;
    public long ScheduleId;
}

//api : add_schedule
public class AddSchedule : WebSocketBehavior
{
    private IScheduleProvider _scheduleProvider;
    private ICinemaProvider _cinemaProvider;
    private IFilmProvider _filmProvider;

    public void Set
    (
        IScheduleProvider scheduleProvider,
        ICinemaProvider cinemaProvider,
        IFilmProvider filmProvider
    )
    {
        _scheduleProvider = scheduleProvider;
        _cinemaProvider = cinemaProvider;
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"add_schedule req:\n{e.Data}");

        var req = JsonHelper.Parse<AddScheduleReq>(e.Data);
        var film = _filmProvider.GetFilm(req.ScheduleFilmId);
        var cinema = _cinemaProvider.GetCinema(req.ScheduleCinemaId);

        AddScheduleRsp rsp;

        if (film != null && cinema != null)
        {
            var schedule = _scheduleProvider.CreateSchedule
            (
                req.ScheduleStartTime,
                req.ScheduleEndTime,
                cinema,
                film
            );
            if (schedule != null)
            {
                rsp = new AddScheduleRsp
                {
                    Ok = true,
                    ScheduleId = schedule.Id
                };
            }
            else
            {
                rsp = new AddScheduleRsp
                {
                    Ok = false,
                    ScheduleId = 0
                };
            }
        }
        else
        {
            rsp = new AddScheduleRsp
            {
                Ok = false,
                ScheduleId = 0
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"add_schedule rsp:\n{json}");
        Send(json);
    }
}