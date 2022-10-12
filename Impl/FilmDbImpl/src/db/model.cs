using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMovie.Db.Film.Model
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
}
