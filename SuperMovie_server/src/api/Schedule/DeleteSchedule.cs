using SuperMovie.Container.Schedule.Provider;

namespace SuperMovie.Server.Api.Schedule;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct DeleteScheduleReq
{
    public long ScheduleId;
}

public struct DeleteScheduleRsp
{
    public bool Ok;
}

//api : delete_schedule
public class DeleteSchedule : WebSocketBehavior
{
    private IScheduleProvider _scheduleProvider;

    public void Set(IScheduleProvider scheduleProvider)
    {
        _scheduleProvider = scheduleProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"delete_schedule req:\n{e.Data}");

        var req = JsonHelper.Parse<DeleteScheduleReq>(e.Data);
        var schedule = _scheduleProvider.GetSchedule(req.ScheduleId);

        DeleteScheduleRsp rsp;

        if (schedule != null && schedule.Drop())
        {
            rsp = new DeleteScheduleRsp
            {
                Ok = true
            };
        }
        else
        {
            rsp = new DeleteScheduleRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"delete_schedule rsp:\n{json}");
        Send(json);
    }
}