namespace SuperMovie.Server.Api.Schedule;

using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Schedule.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllScheduleByCinemaIdReq
{
    public long CinemaId;
}

public struct ScheduleRsp
{
    public long ScheduleId;
    public long ScheduleFilmId;
    public DateTime ScheduleStartTime;
    public DateTime ScheduleEndTime;
}

public struct GetAllScheduleByCinemaIdRsp
{
    public List<ScheduleRsp> Collection;
}

//api : get_all_schedule_by_cinema_id
public class GetAllScheduleByCinemaId : WebSocketBehavior
{
    private IScheduleProvider _scheduleProvider;
    private ICinemaProvider _cinemaProvider;

    public void Set
    (
        IScheduleProvider scheduleProvider,
        ICinemaProvider cinemaProvider
    )
    {
        _scheduleProvider = scheduleProvider;
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_schedule_by_cinema_id req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllScheduleByCinemaIdReq>(e.Data);
        var scheduleRspList = new List<ScheduleRsp>();

        var cinema = _cinemaProvider.GetCinema(req.CinemaId);
        if (cinema != null)
        {
            foreach (var schedule in _scheduleProvider.FilterScheduleByCinema(cinema))
            {
                if (schedule.StartTime != null)
                {
                    var it = new ScheduleRsp
                    {
                        ScheduleId = schedule.Id,
                        ScheduleFilmId = schedule.Film.Id,
                        ScheduleStartTime = schedule.StartTime.Value,
                        ScheduleEndTime = schedule.EndTime.Value
                    };
                    scheduleRspList.Add(it);
                }
            }
        }

        var rsp = new GetAllScheduleByCinemaIdRsp
        {
            Collection = scheduleRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_schedule_by_cinema_id rsp:\n{json}");
        Send(json);
    }
}