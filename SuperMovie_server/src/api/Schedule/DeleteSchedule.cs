namespace SuperMovie.Server.Api.Schedule;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}