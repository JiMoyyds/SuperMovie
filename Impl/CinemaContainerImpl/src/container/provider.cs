using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Db.Film.Operation;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Db.Cinema.Model;
using SuperMovie.Db.Cinema.Operation;
using SuperMovie.Util;
using DbMiddleware;
using DbMiddlewarePostgresImpl;

namespace SuperMovie.Container.Cinema.Provider
{
    public class CinemaProvider : ICinemaProvider
    {
        Func<IScheduleProvider> providerF;
        IdGenerator gen;
        IDatabase Db;

        public CinemaProvider(Func<IScheduleProvider> scheduleProviderF, IdGenerator Gen,IDatabase db)
        {
            providerF = scheduleProviderF;
            gen = Gen;
            Db = db;
        }
        public ICinemaEntity? CreateCinema(string name)
        {
            var cinemaid = gen.Next();
            var db = new CinemaOperation(Db);
            CinemaModel model = new CinemaModel(cinemaid, name);
            var result = db.CreateCinema(model);
            if (result)
            {
                ICinemaEntity cinema = new CinemaEntity(cinemaid, providerF,Db);
                return cinema;
            }
            else return null;

        }

        public ICinemaEntity? GetCinema(long id)
        {
            var db = new CinemaOperation(Db);
            var result = db.GetCinema(id);
            if (result != null)
            {
                ICinemaEntity cinema = new CinemaEntity(id, providerF,Db);
                return cinema;
            }
            else return null;
        }

        public List<ICinemaEntity> GetAllCinema()
        {
            var db = new CinemaOperation(Db);
            var list = db.GetAllCinema();
            List<ICinemaEntity> result = new List<ICinemaEntity>();
            foreach (var item in list)
            {
                var cinema = GetCinema(item.Id);
                if (cinema != null)
                    result.Add(cinema);
            }
            return result;
        }
    }
}
