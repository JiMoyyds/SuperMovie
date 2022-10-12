using SuperMovie.Container.Film.Entity;
using SuperMovie.Db.Film.Model;
using SuperMovie.Db.Film.Operation;
using SuperMovie.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMiddleware;

namespace SuperMovie.Container.Film.Provider
{
    public class FilmProvider : IFilmProvider
    {
        public IDatabase Db;
        public IdGenerator gen;
        public FilmProvider(IdGenerator gen, IDatabase db)
        {
            this.gen = gen;
            Db = db;
        }
        public IFilmEntity? CreateFilm
            (string name,
            DateTime onlineTime,
            bool isPreorder,
            double price)
        {
            var model = new Db.Film.Model.FilmModel(gen.Next(), name, onlineTime, isPreorder, new List<string> (), price, new List<string>(), "", "");
            IFilmOperation op = new FilmOperation(Db);
            bool judgement = op.CreateFilm(model);
            if (judgement)
            {
                IFilmEntity? film = GetFilm(model.Id);
                return film;
            }
            else return null;
        }

        public IFilmEntity? GetFilm(long id)
        {
            FilmOperation op = new FilmOperation(Db);
            var result0 = op.GetFilm(id);
            if (result0 != null)
            {
                FilmEntity result1 = new FilmEntity(result0.Id, Db);

                return result1;
            }
            else return null;
        }
        public List<IFilmEntity> GetAllFilm()
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.GetAllFilm();
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }

        public List<IFilmEntity> FilterFilmByIsPreorder(bool isPreorder)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.FilterFilmByIsPreorder(isPreorder);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }
        public List<IFilmEntity> FilterFilmByTypes(List<string> types)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.FilterFilmByTypes(types);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }
        public List<IFilmEntity> FilterFilmByOnlineTime(DateTime start, DateTime end)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.FilterFilmByOnlineTime(start, end);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }
        public List<IFilmEntity> FilterFilmByName(string name)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.FilterFilmByName(name);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }
        public List<IFilmEntity> MatchFilmByName(string name)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.MatchFilmByName(name);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }
        public List<IFilmEntity> FilterFilmByActors(List<string> actors)
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.FilterFilmByActors(actors);
            if (filmModels != null)
            {
                List<IFilmEntity> result = new List<IFilmEntity>();
                foreach (IFilmModel filmModel in filmModels)
                {
                    FilmEntity result1 = new FilmEntity(filmModel.Id, Db);
                    result.Add(result1);
                }
                return result;
            }
            else return new List<IFilmEntity>();
        }

        public List<string> GetAllFilmType()
        {

            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.GetAllFilm();
            var result1 = new List<string>();
            if (filmModels != null)
            {
                string s="1";
                List<string> result = new List<string>();
                foreach (var item in filmModels)
                {
                   foreach(var item1 in item.Types)
                    {
                        result.Add(item1);
                        s = item1;
                    }
                } 
                if (s !="")
                {
                    foreach (string item in result)
                    {
                        if (!result1.Contains(item))
                        {
                            result1.Add(item);
                        }
                    }
                    return result1;
                }
                else return result1;
            }
            else return result1;

        }
            
        public List<DateTime> GetAllFilmOnlineTime()
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.GetAllFilm();
            if (filmModels != null)
            {
                List<DateTime> result = new List<DateTime>();
                foreach(var item in filmModels)
                {
                    result.Add( item.OnlineTime);
                }
                var result1 =new List<DateTime>();
                foreach(DateTime item in result)
                {
                    if (!result1.Contains(item))
                    {
                        result1.Add(item);
                    }
                }
                return result1;
            }
            else return new List<DateTime>();
        }



    }
}
