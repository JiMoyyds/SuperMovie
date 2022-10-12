using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Db.Schedule.Model;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Db.Schedule.Model
{
    public class ScheduleModel : IScheduleModel
    {
        public ScheduleModel(long cinemaid, long filmid, DateTime starttime, DateTime endtime, long id)
        {
            Id = id;
            CinemaId = cinemaid;
            FilmId = filmid;
            StartTime = starttime;
            EndTime = endtime;
        }
        public long CinemaId { get; set; }
        public long FilmId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Id { get; set; }
    }
}
