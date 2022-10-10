namespace SuperMovie.Server.Api.Schedule;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddScheduleReq
{
    public long ScheduleFilmId;
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
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddScheduleReq>(e.Data);
        var rsp = new AddScheduleRsp
        {
            Ok = false,
            ScheduleId = 0
        };
    }
}