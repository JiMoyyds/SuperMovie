using SuperMovie.Util;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Schedule.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Container.Schedule.Entity
{
    public class ScheduleEntity : IScheduleEntity
    {
        public long id;
        public Func<ICinemaProvider> cinemaF;
        public IFilmProvider film;
        public IDatabase Db;

        internal ScheduleEntity(Func<ICinemaProvider> cinemaprovider, IFilmProvider filmprovider, long id,IDatabase db)
        {
            cinemaF = cinemaprovider;
            film = filmprovider;
            this.id = id;
            Db = db;
        }
        public bool Drop()
        {
            var dbschedule = new ScheduleOperation(Db);
            var result_schedule = dbschedule.DeleteSchedule(Id);
            if (result_schedule != false)
            {
                return true;
            }
            else return false;
        }

        public bool IsValid()
        {
            var db = new ScheduleOperation(Db);
            var resultdb = db.GetSchedule(Id);//数据库含有该条数据
            if (resultdb != null)
            {
                return true;
            }
            else return false;
        }

        public long Id
        {
            get
            {
                return id;
            }
        }
        public DateTime? StartTime
        {
            get
            {
                var db = new ScheduleOperation(Db);
                var result = db.GetSchedule(Id);
                if (result != null)
                {
                    var time = result.StartTime;
                    return time;
                }
                else return null;
            }
            set
            {
                var db = new ScheduleOperation(Db);
                if (value != null)
                {
                    db.UpdateScheduleStartTime(Id, (DateTime)value);
                }
            }
        }
        public DateTime? EndTime
        {
            get
            {
                var db = new ScheduleOperation(Db);
                var result = db.GetSchedule(Id);
                if (result != null)
                {
                    var time = result.EndTime;
                    return time;
                }
                else return null;
            }
            set
            {
                var db = new ScheduleOperation(Db);
                if (value != null)
                {
                    db.UpdateScheduleEndTime(Id, (DateTime)value);
                }
            }
        }
        public ICinemaEntity? Cinema
        {
            get
            {
                var db = new ScheduleOperation(Db);
                var result = db.GetSchedule(Id);
                if (result != null)
                {
                    var cinemaentity = cinemaF().GetCinema(result.CinemaId);
                    if (cinemaentity != null)
                        return cinemaentity;
                    else return null;
                }
                else return null;
            }
            set
            {
                var db = new ScheduleOperation(Db);
                if (value != null)
                {
                    db.UpdateScheduleCinemaId(Id, value.Id);
                }
            }
        }
        public IFilmEntity? Film
        {
            get
            {
                var db = new ScheduleOperation(Db);
                var result = db.GetSchedule(Id);
                if (result != null)
                {
                    var filmentity = film.GetFilm(result.FilmId);
                    if (filmentity != null)
                        return filmentity;
                    else return null;
                }
                else return null;
            }
            set
            {
                var db = new ScheduleOperation(Db);
                if (value != null)
                {
                    db.UpdateScheduleFilmId(Id, value.Id);
                }
            }
        }
    }
}
