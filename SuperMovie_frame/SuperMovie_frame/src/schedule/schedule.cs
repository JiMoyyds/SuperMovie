namespace SuperMovieSDK.Schedule.Operation;

using Model;

//由wzj实现
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