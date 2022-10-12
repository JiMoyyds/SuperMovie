using DbMiddleware;
using DbMiddlewarePostgresImpl;
using SuperMovie.Db.Film.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMovie.Db.Film.Operation
{
    public class FilmModel : IFilmModel
    {
        public FilmModel
            (
            long id,
            string name,
            DateTime onlineTime,
            bool isPreorder,
            List<string> types,
            double price,
            List<string> actors,
            string coverurl,
            string previewvideourl
            )
        {
            Id = id;
            Name = name;
            OnlineTime = onlineTime;
            IsPreorder = isPreorder;
            Types = types;
            Price = price;
            Actors = actors;
            CoverUrl = coverurl;
            PreviewVideoUrl = previewvideourl;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime OnlineTime { get; set; }
        public bool IsPreorder { get; set; }
        public List<string> Types { get; set; }
        public double Price { get; set; }
        public List<string> Actors { get; set; }
        public string CoverUrl { get; set; }
        public string PreviewVideoUrl { get; set; }
    }

    public class FilmOperation : IFilmOperation
    {
        public IDatabase Db;
        public FilmOperation(IDatabase db)
        {
            Db = db;
        }

        public bool CreateFilm(IFilmModel model)
        {
            string str1="";
            if (model.Types.Count != 0)
            {
                foreach (string str in model.Types)
                {
                    if (str != "") str1 += ("$" + str);
                }
                if (str1 != "") str1 = str1.Substring(1);
            }
            else 
                str1 = "";
            string str2 = "";
            if (model.Actors.Count != 0)
            {
                foreach (string str in model.Actors)
                {
                    if (str != "") str2 += ("$" + str);
                }
                if (str2 != "") str2 = str2.Substring(1);
            }
            else 
                str2 = "";
            int judgement = Db.Query(
                @"INSERT INTO default_schema.film 
                ( film_id, film_name, film_online_time, film_is_preorder, film_types, film_price, film_actors,film_cover_url,film_preview_video_url) 
                VAlUES 
                (:film_id,:film_name,:film_online_time,:film_is_preorder,:film_types,:film_price,:film_actors,:film_cover_url,:film_preview_video_url)",
                1,
            new[] {
            ("film_id", model.Id),
            ("film_name", model.Name),
            ("film_online_time", model.OnlineTime),
            ("film_is_preorder", model.IsPreorder),
            ("film_types",str1),
            ("film_price",model.Price),
            ("film_actors",str2),
            ("film_cover_url",model.CoverUrl),
            ("film_preview_video_url",(object)model.PreviewVideoUrl)
            });
            if (judgement == 1) return true;
            else return false;
        }

        public bool DeleteFilm(long filmId)
        {
            int judgement = Db.Query("DELETE FROM default_schema.film WHERE film_id=:film_id;", -1,
            new[] { ("film_id", (object)filmId) });
            if (judgement > 0) return true;
            else return false;
        }
        public List<IFilmModel> GetAllFilm()
        {
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film", null);
            if (result != null)
            {
                List<IFilmModel> list = new List<IFilmModel>();
                foreach (Dictionary<string, object> row in result)
                {
                    List<string> list1;
                    List<string> list2;
                    row.TryGetValue("film_types", out object? num1);
                    row.TryGetValue("film_types", out object? num2);
                    if (num1.ToString() == "")
                    {
                        list1 = new List<string>();
                    }
                    else
                        list1 = new List<string>(row["film_types"].ToString().Split('$'));
                    if (num2.ToString() == "")
                    {
                        list2 = new List<string>();
                    }
                    else
                        list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                    var model = new FilmModel(
                        (long)row["film_id"],
                        (string)row["film_name"],
                        (DateTime)row["film_online_time"],
                        (bool)row["film_is_preorder"],
                        list1,
                        (double)row["film_price"],
                        list2,
                        (string)row["film_cover_url"],
                        (string)row["film_preview_video_url"]
                        );
                    list.Add(model);
                }
                return list;
            }
            else return new List<IFilmModel>();
        }

        public List<IFilmModel> FilterFilmByIsPreorder(bool isPreorder)
        {
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_is_preorder=:film_is_preorder ",
                new[] { ("film_is_preorder", (object)isPreorder) });
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> row in result)
            {
                List<string> list1 = new List<string>(row["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                var model = new FilmModel(
                    (long)row["film_id"],
                    (string)row["film_name"],
                    (DateTime)row["film_online_time"],
                    (bool)row["film_is_preorder"],
                    list1,
                    (double)row["film_price"],
                    list2,
                    (string)row["film_cover_url"],
                    (string)row["film_preview_video_url"]
                    );
                list.Add(model);
            }
            return list;
        }

        public List<IFilmModel> FilterFilmByTypes(List<string> types)
        {
            string types_str1 = "";
            foreach (var it1 in types)
            {
                types_str1 += ("$" + it1);
            }
            types_str1 = types_str1.Substring(1);
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_types LIKE :film_types ",
                new[] { ("film_types", (object)('%'+types_str1+'%')) });
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> row in result)
            {
                List<string> list1 = new List<string>(row["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                var model = new FilmModel(
                    (long)row["film_id"],
                    (string)row["film_name"],
                    (DateTime)row["film_online_time"],
                    (bool)row["film_is_preorder"],
                    list1,
                    (double)row["film_price"],
                    list2,
                    (string)row["film_cover_url"],
                    (string)row["film_preview_video_url"]
                    );
                list.Add(model);
            }
            return list;
        }

        public List<IFilmModel> FilterFilmByOnlineTime(DateTime start, DateTime end)
        {
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_online_time>=:time_start AND film_online_time<=:time_end",
                new[] { ("time_start", (object)start),
                ("time_end",(object)end)});
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> row in result)
            {
                List<string> list1 = new List<string>(row["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                var model = new FilmModel(
                    (long)row["film_id"],
                    (string)row["film_name"],
                    (DateTime)row["film_online_time"],
                    (bool)row["film_is_preorder"],
                    list1,
                    (double)row["film_price"],
                    list2,
                    (string)row["film_cover_url"],
                    (string)row["film_preview_video_url"]
                    );
                list.Add(model);
            }
            return list;
        }

        public List<IFilmModel> FilterFilmByName(string name)
        {
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_name=:film_name ",
                new[] { ("film_name", (object)name) });
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> row in result)
            {
                List<string> list1 = new List<string>(row["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                var model = new FilmModel(
                    (long)row["film_id"],
                    (string)row["film_name"],
                    (DateTime)row["film_online_time"],
                    (bool)row["film_is_preorder"],
                    list1,
                    (double)row["film_price"],
                    list2,
                    "",
                    ""
                    );
                list.Add(model);
            }
            return list;
        }

        public List<IFilmModel> FilterFilmByActors(List<string> actors)
        {
            string types_str2 = "";
            foreach (var it1 in actors)
            {
                types_str2 += ("$" + it1);
            }
             types_str2 = types_str2.Substring(1);
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_actors LIKE :film_actors",
                new[] { ("film_actors", (object)('%'+types_str2+'%')) });
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> row in result)
            {
                List<string> list1 = new List<string>(row["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(row["film_actors"].ToString().Split('$'));
                var model = new FilmModel(
                    (long)row["film_id"],
                    (string)row["film_name"],
                    (DateTime)row["film_online_time"],
                    (bool)row["film_is_preorder"],
                    list1,
                    (double)row["film_price"],
                    list2,
                    (string)row["film_cover_url"],
                    (string)row["film_preview_video_url"]
                    );
                list.Add(model);
            }
            return list;
        }

        public bool UpdateFilmName(long filmId, string newValue)
        {
            int judgement = Db.Query("UPDATE super_movie.default_schema.film SET film_name=:film_name WHERE film_id=:film_id", 1,
                new[] {("film_id",(object)filmId),
            ("film_name",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmOnlineTime(long filmId, DateTime newValue)
        {
            int judgement = Db.Query("UPDATE super_movie.default_schema.film SET film_online_time=:film_online_time WHERE film_id=:film_id", 1,
                new[] {("film_id",filmId),
                 ("film_online_time",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmIsPreorder(long filmId, bool newValue)
        {
            int judgement = Db.Query("UPDATE super_movie.default_schema.film SET film_is_preorder=:film_is_preorder WHERE film_id=:film_id", 1,
                new[] {("film_id",filmId),
            ("film_is_preorder",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmTypes(long filmId, List<string> newValue)
        {
            //string str1 = string.Join("$", newValue);
            string str1 = "";
            foreach (string str in newValue)
            {
                if (str != "") str1 += ("$" + str);
            }
            if (str1 != "") str1 = str1.Substring(1);
            int judgement = Db.Query("UPDATE default_schema.film SET film_types=:film_types WHERE film_id=:film_id", 1,
                new[] { ("film_types", (object)str1) ,
                ("film_id",(object)filmId)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmPrice(long filmId, double newValue)
        {
            int judgement = Db.Query("UPDATE default_schema.film SET film_price=:film_price WHERE film_id=:film_id", 1,
                new[] {("film_id",(object)filmId),
            ("film_price",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmActors(long filmId, List<string> newValue)
        {
            //string str1 = string.Join("$", newValue);
            string str1 = "";
            foreach (string str in newValue)
            {
                if (str != "") str1 += ("$" + str);
            }
            if (str1 != "") str1 = str1.Substring(1);
            int judgement = Db.Query("UPDATE default_schema.film SET film_actors=:film_actors WHERE film_id=:film_id", 1,
                new[] { ("film_actors", (object)str1) ,
                ("film_id",(object)filmId)});
            if (judgement == 1) return true;
            else return false;
        }

        public IFilmModel? GetFilm(long filmId)
        {
            var result = Db.QueryForFirstRow("SELECT * FROM default_schema.film WHERE film_id=:film_id ",
                new[] { ("film_id", (object)filmId) });
            List<string> list1;
            List<string> list2;
            result.TryGetValue("film_types", out object? num1);
            result.TryGetValue("film_actors", out object? num2);
            if (num1.ToString()=="")
            {
                list1 = new List<string>();
            }
            else
                list1 = new List<string>(result["film_types"].ToString().Split('$'));
            if(num2.ToString() == "")
            {
                list2 = new List<string>();
            } 
            else
                list2 = new List<string>(result["film_actors"].ToString().Split('$'));
            IFilmModel filmModel = new FilmModel(
                (long)result["film_id"],
                (string)result["film_name"],
                (DateTime)result["film_online_time"],
                (bool)result["film_is_preorder"],
                list1,
                (double)result["film_price"],
                list2,
                (string)result["film_cover_url"],
                (string)result["film_preview_video_url"]
                );
            if (filmModel != null) return filmModel;
            else return null;

        }

        public List<IFilmModel> MatchFilmByName(string name)
        {
            List<Dictionary<string, object>> result = Db.QueryForTable("SElECT * FROM default_schema.film WHERE film_name LIKE :film_name",
                new[] { ("film_name", (object)('%' + name + '%')) });
            List<IFilmModel> list = new List<IFilmModel>();
            foreach (Dictionary<string, object> item in result)
            {
                List<string> list1 = new List<string>(item["film_types"].ToString().Split('$'));
                List<string> list2 = new List<string>(item["film_actors"].ToString().Split('$'));
                IFilmModel filmModel = new FilmModel
                (
                    (long)item["film_id"],
                    (string)item["film_name"],
                    (DateTime)item["film_online_time"],
                    (bool)item["film_is_preorder"],
                    list1,
                    (double)item["film_price"],
                    list2,
                    (string)item["film_cover_url"],
                    (string)item["film_preview_video_url"]
                );
                list.Add(filmModel);
            }
            return list;
        }

        public List<string> GetAllActors()
        {
            FilmOperation op = new FilmOperation(Db);
            List<IFilmModel> filmModels = op.GetAllFilm();
            var result1 = new List<string>();
            string s = "1";
            if (filmModels != null)
            {
                List<string> result = new List<string>();
                foreach (var item in filmModels)
                {
                    foreach (var item1 in item.Actors)
                    {
                        result.Add(item1);
                        s = item1;
                    }
                }
                if (s!="")
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
                else return new List<string>();
            }
            else return new List<string>();
        }

        public bool UpdateFilmCoverUrl(long filmId, string newValue)
        {
            int judgement = Db.Query("UPDATE default_schema.film SET film_cover_url=:film_cover_url WHERE film_id=:film_id", 1,
                new[] {("film_id",(object)filmId),
            ("film_cover_url",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
        public bool UpdateFilmPreviewVideoUrl(long filmId, string newValue)
        {
            int judgement = Db.Query("UPDATE default_schema.film SET film_preview_video_url=:film_preview_video_url WHERE film_id=:film_id", 1,
                new[] {("film_id",(object)filmId),
            ("film_preview_video_url",(object)newValue)});
            if (judgement == 1) return true;
            else return false;
        }
    }
}
