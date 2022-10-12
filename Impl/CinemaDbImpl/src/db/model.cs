using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Db.Schedule.Model;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Db.Cinema.Model
{
    public class CinemaModel : ICinemaModel
    {
        public CinemaModel(long id, String name)
        {
            Id = id;
            Name = name;
        }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
