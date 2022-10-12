using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Schedule.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Util;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Container.Cinema.Entity
{
    public class CinemaEntity : ICinemaEntity
    {
        public long id;
        public Func<IScheduleProvider> providerF;
        private IDatabase Db;

        internal CinemaEntity(long id, Func<IScheduleProvider> scheduleprovider,IDatabase db)
        {
            this.id = id;
            providerF = scheduleprovider;
            Db = db;
        }

        public long Id
        {
            get
            {
                return id;
            }
        }

        public string? Name
        {
            get
            {
                var db = new CinemaOperation(Db);
                var result = db.GetCinema(Id);
                if (result != null)
                    return result.Name;
                else return null;
            }
            set
            {
                var db = new CinemaOperation(Db);
                if (value != null)
                {
                    var result = db.UpdateCinemaName(Id, (string)value);
                }
            }
        }

        public List<IScheduleEntity> Schedules
        {
            get
            {
                var db = new ScheduleOperation(Db);
                var list = db.GetAllSchedule();
                List<IScheduleEntity> entity = new List<IScheduleEntity>();
                foreach (var item in list)
                {
                    var schedule = providerF().GetSchedule(item.Id);
                    if (schedule != null)
                        entity.Add(schedule);
                }
                return entity;
            }
        }

        public bool Drop()
        {
            bool flag = false;
            var dbcinema = new CinemaOperation(Db);
            var resultcinema = dbcinema.DeleteCinema(id);
            var dbschedule = new ScheduleOperation(Db);
            var resultschedule = dbschedule.FilterScheduleByCinemaId(id);
            foreach (var item in resultschedule)
            {
                flag = dbschedule.DeleteSchedule(item.Id);
                if (flag == false) break;
            }
            if (resultcinema&&flag) return true;
            else return false;
        }

        public bool IsValid()
        {
            var db = new CinemaOperation(Db);
            var result = db.GetCinema(id);
            if (result != null) return true;
            else return false;
        }

        public bool ClearSchedule()
        {
            var db = new ScheduleOperation(Db);
            var result = db.GetAllSchedule();
            foreach (var item in result)
            {
                var flag = db.DeleteSchedule(item.Id);
                if (flag == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
