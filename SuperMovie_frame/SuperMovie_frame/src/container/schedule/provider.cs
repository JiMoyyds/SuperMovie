namespace SuperMovie.Container.Schedule.Provider;

using Entity;
using Film.Entity;
using Cinema.Entity;

//resolver : WZJ
//asm file : ScheduleContainerImpl
//impl :: SuperMovie.Container.Schedule.Provider.ScheduleProvider
//impl proj struct :
//src/container/provider.cs
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