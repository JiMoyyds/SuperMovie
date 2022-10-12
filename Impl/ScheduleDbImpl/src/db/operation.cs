using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Db.Schedule.Model;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Db.Schedule.Operation
{
    public class ScheduleOperation : IScheduleOperation
    {
        private IDatabase Db;
        public ScheduleOperation(IDatabase db)
        {
            Db = db;
        }
        public bool CreateSchedule(IScheduleModel model)
        {
            var sql = "INSERT INTO default_schema.schedule (" +
                "schedule_cinema_id, schedule_film_id, schedule_start_time, schedule_end_time,schedule_id) " +
                "VALUES(:schedule_cinema_id, :schedule_film_id, :schedule_start_time, :schedule_end_time, :schedule_id);";

            var flag = Db.Query(sql, 1, new[] { ("schedule_cinema_id",(object)model.CinemaId),("schedule_film_id",(object)model.FilmId),
            ("schedule_start_time",(object)model.StartTime),("schedule_end_time",(object)model.EndTime),("schedule_id",(object)model.Id)});

            if (flag == 1) return true;
            else return false;
        }

        public bool DeleteSchedule(long scheduleId)
        {
            var sql = "DELETE FROM default_schema.schedule WHERE schedule_id = :schedule_id";
            var flag = Db.Query(sql, 1, new[] { ("schedule_id", (Object)scheduleId) });
            if (flag == 1) return true;
            else return false;
        }

        public IScheduleModel? GetSchedule(long scheduleId)
        {
            var sql = "SELECT * FROM default_schema.schedule WHERE schedule_id=:schedule_id";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, new[] { ("schedule_id", (object)scheduleId) });
            if (result.Count == 0) 
            {
                return null;
            }
            else
            {
                List<IScheduleModel> list = new List<IScheduleModel>();
                foreach (Dictionary<string, object> row in result)
                {
                    var cinema = new ScheduleModel((long)row["schedule_cinema_id"], (long)row["schedule_film_id"],
                    (DateTime)row["schedule_start_time"], (DateTime)row["schedule_end_time"], (long)row["schedule_id"]);
                    list.Add(cinema);
                }
                ScheduleModel model = new ScheduleModel(list[0].CinemaId, list[0].FilmId, list[0].StartTime, list[0].EndTime, list[0].Id);
                return model;
            }
        }

        public List<IScheduleModel> GetAllSchedule()
        {
            var sql = "SELECT * FROM default_schema.schedule";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, null);
            List<IScheduleModel> list = new List<IScheduleModel>();
            foreach (Dictionary<string, object> row in result)
            {
                var schedule = new ScheduleModel((long)row["schedule_cinema_id"], (long)row["schedule_film_id"],
                    (DateTime)row["schedule_start_time"], (DateTime)row["schedule_end_time"], (long)row["schedule_id"]);
                list.Add(schedule);
            }
            return list;
        }

        public List<IScheduleModel> FilterScheduleByCinemaId(long cinemaId)
        {
            var sql = "SELECT * FROM default_schema.schedule WHERE schedule_cinema_id=:schedule_cinema_id";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, new[] { ("schedule_cinema_id", (object)cinemaId) });
            List<IScheduleModel> list = new List<IScheduleModel>();
            if (result.Count == 0)
            {
                return list;
            }
            else
            {
                foreach (Dictionary<string, object> row in result)
                {
                    var schedule = new ScheduleModel((long)row["schedule_cinema_id"], (long)row["schedule_film_id"],
                        (DateTime)row["schedule_start_time"], (DateTime)row["schedule_end_time"], (long)row["schedule_id"]);
                    list.Add(schedule);
                }
                return list;
            }
        }

        public List<IScheduleModel> FilterScheduleByTimespan(DateTime start, DateTime end)
        {
            var sql = "SELECT * FROM default_schema.schedule";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, null);
            List<IScheduleModel> list = new List<IScheduleModel>();
            foreach (Dictionary<string, object> row in result)
            {
                if((DateTime)row["schedule_end_time"]>start&& (DateTime)row["schedule_end_time"]<end)
                {
                    var schedule = new ScheduleModel((long)row["schedule_cinema_id"], (long)row["schedule_film_id"],
    (DateTime)row["schedule_start_time"], (DateTime)row["schedule_end_time"], (long)row["schedule_id"]);
                    list.Add(schedule);
                }
            }
            return list;
        }

        public bool UpdateScheduleCinemaId(long scheduleId, long newValue)
        {
            var sql = "UPDATE default_schema.schedule SET schedule_cinema_id=:schedule_cinema_id WHERE schedule_id=:schedule_id";
            var result = Db.Query(sql, 1, new[] { ("schedule_cinema_id", (object)newValue), ("schedule_id", (object)scheduleId) });
            if (result == 1) return true;
            else return false;
        }

        public bool UpdateScheduleFilmId(long scheduleId, long newValue)
        {
            var sql = "UPDATE default_schema.schedule SET schedule_film_id=:schedule_film_id WHERE schedule_id=:schedule_id";
            var result = Db.Query(sql, 1, new[] { ("schedule_film_id", (object)newValue), ("schedule_id", (object)scheduleId) });
            if (result == 1) return true;
            else return false;
        }

        public bool UpdateScheduleStartTime(long scheduleId, DateTime newValue)
        {
            var sql = "UPDATE default_schema.schedule SET schedule_start_time=:schedule_start_time WHERE schedule_id=:schedule_id";
            var result = Db.Query(sql, 1, new[] { ("schedule_start_time", (object)newValue), ("schedule_id", (object)scheduleId) });
            if (result == 1) return true;
            else return false;
        }

        public bool UpdateScheduleEndTime(long scheduleId, DateTime newValue)
        {
            var sql = "UPDATE default_schema.schedule SET schedule_end_time=:schedule_end_time WHERE schedule_id=:schedule_id";
            var result = Db.Query(sql, 1, new[] { ("schedule_end_time", (object)newValue), ("schedule_id", (object)scheduleId) });
            if (result == 1) return true;
            else return false;
        }
    }
}
