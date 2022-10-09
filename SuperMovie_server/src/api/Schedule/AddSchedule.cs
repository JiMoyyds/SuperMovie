namespace SuperMovie.Server.Api.Schedule;

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
public class AddSchedule
{
}