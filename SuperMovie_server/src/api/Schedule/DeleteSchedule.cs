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
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<DeleteScheduleReq>(e.Data);
        var rsp = new DeleteScheduleRsp
        {
            Ok = false
        };
       
        Send(JsonHelper.Stringify(rsp));
    }
}