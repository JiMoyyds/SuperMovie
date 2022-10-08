namespace SuperMovie.Db.Schedule.Operation;

using Model;

//resolver : WZJ
//asm file : ScheduleDbImpl
//impl :: SuperMovie.Db.Schedule.Operation.ScheduleOperation
//impl proj struct :
//src/db/operation.cs
public interface IScheduleOperation
{
    public bool CreateSchedule(IScheduleModel model);
    public bool DeleteSchedule(long scheduleId);
    public IScheduleModel? GetSchedule(long scheduleId);
    public List<IScheduleModel> GetAllSchedule();
    public List<IScheduleModel> FilterScheduleByCinemaId(long cinemaId);
    public List<IScheduleModel> FilterScheduleByTimespan(DateTime start, DateTime end);
    public bool UpdateScheduleCinemaId(long scheduleId, long newValue);
    public bool UpdateScheduleFilmId(long scheduleId, long newValue);
    public bool UpdateScheduleStartTime(long scheduleId, DateTime newValue);
    public bool UpdateScheduleEndTime(long scheduleId, DateTime newValue);
}