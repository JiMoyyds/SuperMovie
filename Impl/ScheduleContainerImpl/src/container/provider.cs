using SuperMovie.Util;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Schedule.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Film.Entity;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Container.Schedule.Provider
{
    public class ScheduleProvider : IScheduleProvider
    {
        public Func<ICinemaProvider> CinemaProviderF;
        public IFilmProvider FilmProvider;
        public IdGenerator Gen;
        public IDatabase Db;
        public ScheduleProvider(Func<ICinemaProvider> cinemaProvider, IFilmProvider filmProvider, IdGenerator gen,IDatabase db)
        {
            CinemaProviderF = cinemaProvider;
            FilmProvider = filmProvider;
            Gen = gen;
            Db = db;
        }

        public IScheduleEntity? CreateSchedule
        (
        DateTime startTime,
        DateTime endTime,
        ICinemaEntity cinema,
        IFilmEntity film
        )
        {
            var cinemaid = cinema.Id;
            var filmid = film.Id;
            var id = Gen.Next();
            if (startTime < endTime)
            {
                var db = new ScheduleOperation(Db);
                var result = db.FilterScheduleByCinemaId(cinema.Id);
                bool judge = true;
                foreach (var item in result)
                {
                    if(((startTime>=item.StartTime)&&(startTime<item.EndTime))||
                        ((endTime>item.StartTime)&&(endTime<=item.EndTime))||
                        ((startTime<=item.StartTime)&&(endTime>=item.EndTime)))
                    {
                        judge = false; break;
                    }
                }
                if (judge)
                {
                    ScheduleModel model = new ScheduleModel(cinemaid, filmid, startTime, endTime, id);
                    if (db.CreateSchedule(model))
                    {
                        IScheduleEntity schedule = new ScheduleEntity(CinemaProviderF, FilmProvider, id, Db);
                        return schedule;
                    }
                    else return null;
                }
                else return null;
            }
            else return null;
        }

        public IScheduleEntity? GetSchedule(long id)
        {
            var db = new ScheduleOperation(Db);
            var result = db.GetSchedule(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                var startTime = result.StartTime;
                var endTime = result.EndTime;
                IScheduleEntity schedule = new ScheduleEntity(CinemaProviderF, FilmProvider, id,Db);
                return schedule;
            }
        }
        public List<IScheduleEntity> GetAllSchedule()
        {
            var db = new ScheduleOperation(Db);
            var list = db.GetAllSchedule();
            List<IScheduleEntity> result = new List<IScheduleEntity>();
            foreach (var item in list)
            {
                var schedule = GetSchedule(item.Id);
                if (schedule != null)
                    result.Add(schedule);
            }
            return result;
        }

        public List<IScheduleEntity> FilterScheduleByCinema(ICinemaEntity cinema)
        {
            var db = new ScheduleOperation(Db);
            var list = db.FilterScheduleByCinemaId(cinema.Id);
            List<IScheduleEntity> result = new List<IScheduleEntity>();
            foreach(var item in list)
            {
                var bottle = new ScheduleEntity(CinemaProviderF,FilmProvider,item.Id,Db);
                result.Add(bottle);
            }
            return result;
        }

        public List<IScheduleEntity> FilterScheduleByTimespan(DateTime start, DateTime end)
        {
            var db = new ScheduleOperation(Db);
            var list = db.FilterScheduleByTimespan(start, end);
            List<IScheduleEntity> result = new List<IScheduleEntity>();
            foreach (var item in list)
            {
                var bottle = new ScheduleEntity(CinemaProviderF, FilmProvider, item.Id, Db);
                result.Add(bottle);
            }
            return result;
        }

    }
}
