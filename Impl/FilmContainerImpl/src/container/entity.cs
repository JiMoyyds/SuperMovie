using SuperMovie.Db.Film.Model;
using SuperMovie.Db.Film.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMiddleware;

namespace SuperMovie.Container.Film.Entity
{
    public class FilmEntity : IFilmEntity
    {
        private long _id;
        IDatabase Db;
        internal FilmEntity(long id, IDatabase db)
        {
            _id = id;
            Db = db;
        }


        /// <summary>
        /// 释放实体
        /// </summary>
        /// <returns></returns>
        public bool Drop()
        {
            FilmOperation op = new FilmOperation(Db);
            bool judgement = op.DeleteFilm(Id);
            if (judgement)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 实体是否合法
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            IFilmOperation op = new FilmOperation(Db);
            IFilmModel? result = op.GetFilm(Id);
            if (result != null) return true;
            else return false;
        }

        public long Id
        {
            get
            {
                return _id;
            }
        }

        public string? Name
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    string? reuslt = result1.Name;

                    if (reuslt != null) return reuslt;
                    else return null;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    IFilmOperation op = new FilmOperation(Db);
                    op.UpdateFilmName(Id, value);
                }
                return;
            }
        }
        public DateTime? OnlineTime
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    DateTime? reuslt = result1.OnlineTime;
                    if (reuslt != null) return reuslt;
                    else return null;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    IFilmOperation op = new FilmOperation(Db);
                    op.UpdateFilmOnlineTime(Id, (DateTime)value);
                }
                return;
            }
        }
        public bool IsPreorder
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    bool reuslt = result1.IsPreorder;
                    return (bool)reuslt;
                }
                return false;
            }
            set
            {
                IFilmOperation op = new FilmOperation(Db);
                op.UpdateFilmIsPreorder(Id, value);

            }
        }

        public List<string> Types
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    List<string> reuslt = result1.Types;
                    if (reuslt.Count!=0) return reuslt;
                    else return new List<string>();
                }
                else return new List<string>();

            }   
        }
        public bool AddType(string name)
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                List<string> list;
                list = result1.Types;
                list.Add(name);
                IFilmOperation op = new FilmOperation(Db);
                bool judgement = op.UpdateFilmTypes(Id, list);
                if (judgement) return true;
                else return false;
            }
            else return false;
        }
        public bool RemoveType(string name)
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                List<string> list = result1.Types;
                list.Remove(name);
                IFilmOperation op = new FilmOperation(Db);
                bool judgement1 = op.UpdateFilmTypes(Id, list);
                if (judgement1) return true;
                else return false;
            }
            else return false;
        }
        public bool ClearType()
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                IFilmOperation op = new FilmOperation(Db);
                List<string> newValue = new List<string>();
                bool judgement = op.UpdateFilmTypes(Id, newValue);
                if (judgement) return true;
                else return false;
            }
            else return false;
        }

        public double Price
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    double reuslt = result1.Price;
                    if (reuslt >= 0)
                    {
                        return (double)reuslt;
                    }
                    return -1;
                }
                else return -1;
            }
            set
            {
                if (value >= 0)
                {
                    IFilmOperation op = new FilmOperation(Db);
                    op.UpdateFilmPrice(Id, value);
                }
                return;
            }
        }

        public List<string> Actors
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    List<string>? reuslt = result1.Actors;
                    if (reuslt != null) return reuslt;
                    else return new List<string>();
                }
                else return new List<string>();
            }
        }
        public bool AddActor(string name)
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                List<string> list;
                list = result1.Actors;
                list.Add(name);
                IFilmOperation op = new FilmOperation(Db);
                bool judgement = op.UpdateFilmActors(Id, list);
                if (judgement) return true;
                else return false;
            }
            else return false;
        }

        public bool RemoveActor(string name)
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                var list = result1.Actors;
                list.Remove(name);
                IFilmOperation op = new FilmOperation(Db);
                bool judgement1 = op.UpdateFilmActors(Id, list);
                if (judgement1) return true;
                else return false;
            }
            else return false;
        }
        public bool ClearActor()
        {
            IFilmOperation op1 = new FilmOperation(Db);
            IFilmModel? result1 = op1.GetFilm(Id);
            if (result1 != null)
            {
                IFilmOperation op = new FilmOperation(Db);
                var list = new List<string>();
                bool judgement = op.UpdateFilmActors(Id, list);
                if (judgement) return true;
                else return false;
            }
            else return false;
        }
        public string CoverUrl
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    string? reuslt = result1.CoverUrl;
                    if (reuslt != null) return reuslt;
                    else return "";
                }
                else return "";
            }
            set
            {
                if (value != null)
                {
                    IFilmOperation op = new FilmOperation(Db);
                    op.UpdateFilmCoverUrl(Id, value);
                }
                return;
            }
        }
        public string PreviewVideoUrl
        {
            get
            {
                IFilmOperation op = new FilmOperation(Db);
                IFilmModel? result1 = op.GetFilm(Id);
                if (result1 != null)
                {
                    string? reuslt = result1.PreviewVideoUrl;
                    if (reuslt != null) return reuslt;
                    else return "";
                }
                else return "";
            }
            set
            {
                if (value != null)
                {
                    IFilmOperation op = new FilmOperation(Db);
                    op.UpdateFilmPreviewVideoUrl(Id, value);
                }
                return;
            }
        }
    }
}
