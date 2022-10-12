namespace SuperMovie.Server.Api.Schedule;

using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Schedule.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetScheduleReq
{
    public long ScheduleId;
}

public struct GetScheduleRsp
{
    public bool Ok;
    public long ScheduleId;
    public long ScheduleCinemaId;
    public long ScheduleFilmId;
    public DateTime ScheduleStartTime;
    public DateTime ScheduleEndTime;
}

//api : get_schedule
public class GetSchedule : WebSocketBehavior
{
    private IScheduleProvider _scheduleProvider;

    public void Set
    (
        IScheduleProvider scheduleProvider
    )
    {
        _scheduleProvider = scheduleProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_schedule req:\n{e.Data}");

        var req = JsonHelper.Parse<GetScheduleReq>(e.Data);
        var schedule = _scheduleProvider.GetSchedule(req.ScheduleId);

        GetScheduleRsp rsp;

        if (schedule != null)
        {
            rsp = new GetScheduleRsp
            {
                Ok = true,
                ScheduleId = schedule.Id,
                ScheduleFilmId = schedule.Film.Id,
                ScheduleStartTime = schedule.StartTime.Value,
                ScheduleEndTime = schedule.EndTime.Value
            };
        }
        else
        {
            rsp = new GetScheduleRsp
            {
                Ok = false,
                ScheduleId = 0,
                ScheduleFilmId = 0,
                ScheduleStartTime = DateTime.Now,
                ScheduleEndTime = DateTime.Now
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_schedule rsp:\n{json}");
        Send(json);
    }
}