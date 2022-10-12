using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Schedule.Operation;
using SuperMovie.Db.Schedule.Model;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Db.Cinema.Operation
{
    public class CinemaOperation : ICinemaOperation
    {
        private IDatabase Db;
        public CinemaOperation(IDatabase db)
        {
            Db = db;
        }
        public bool CreateCinema(ICinemaModel model)
        {
            var sql = "INSERT INTO default_schema.cinema (cinema_id, cinema_name) VALUES(:cinema_id, :cinema_name);";
            var flag = Db.Query(sql, 1, new[] { ("cinema_id", (object)model.Id), ("cinema_name", (object)model.Name) });
            if (flag == 1) return true;
            else return false;
        }

        public bool DeleteCinema(long cinemaId)
        {
            var sql = "DELETE FROM default_schema.cinema WHERE cinema_id = :cinema_id";
            var flag = Db.Query(sql, 1, new[] { ("cinema_id", (object)cinemaId) });
            if (flag == 1) return true;
            else return false;
        }

        public ICinemaModel? GetCinema(long cinemaId)
        {
            var sql = "SELECT * FROM default_schema.cinema WHERE cinema_id = :cinema_id";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, new[] { ("cinema_id", (object)cinemaId) });
            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                List<ICinemaModel> list = new List<ICinemaModel>();
                foreach (Dictionary<string, object> row in result)
                {
                    var cinema = new CinemaModel((long)row["cinema_id"], (String)row["cinema_name"]);
                    list.Add(cinema);
                }
                CinemaModel model = new CinemaModel(list[0].Id, list[0].Name);
                return model;
            }
        }

        public List<ICinemaModel> GetAllCinema()
        {
            var sql = "SELECT * FROM default_schema.cinema";
            List<Dictionary<string, object>> result = Db.QueryForTable(sql, null);
            List<ICinemaModel> list = new List<ICinemaModel>();
            foreach (Dictionary<string, object> row in result)
            {
                var cinema = new CinemaModel((long)row["cinema_id"], (String)row["cinema_name"]);
                list.Add(cinema);
            }
            return list;
        }

        public bool UpdateCinemaName(long cinemaId, string newValue)
        {
            var sql = "UPDATE default_schema.cinema SET cinema_name=:cinema_name WHERE cinema_id=:cinema_id";
            var flag = Db.Query(sql, 1, new[] { ("cinema_name", (object)newValue), ("cinema_id", (object)cinemaId) });
            if (flag == 1) return true;
            else return false;
        }
    }
}
