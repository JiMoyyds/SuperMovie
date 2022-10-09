namespace SuperMovie.Server.Api.Schedule;

public struct AddScheduleReq
{
    public long ScheduleFilmId;
    public DateTime ScheduleStartTime;
    public DateTime ScheduleEndTime;
}

//api : add_schedule
public class AddSchedule
{
}