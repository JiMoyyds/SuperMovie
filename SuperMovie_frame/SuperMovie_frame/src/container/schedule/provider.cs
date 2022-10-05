namespace SuperMovieSDK.Container.Schedule.Provider;

using Entity;
using Film.Entity;
using Cinema.Entity;

public interface IScheduleProvider
{
    public IScheduleEntity? CreateSchedule
    (
        DateTime startTime,
        DateTime endTime,
        ICinemaEntity cinema,
        IFilmEntity film
    );

    public IScheduleEntity? GetSchedule(long id);
    public List<IScheduleEntity> GetAllSchedule();
}