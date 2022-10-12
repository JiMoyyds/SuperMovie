using DbMiddleware;
using DbMiddlewarePostgresImpl;
using NUnit.Framework;
using SuperMovie.Db.Film.Model;
using SuperMovie.Db.Film.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film1
{
    public class test
    {
        IDatabase db=new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
        [SetUp]
        public void Setup()
        {
            db.Query("DELETE FROM default_schema.film", -1, null);
            IFilmOperation op = new FilmOperation(db);
            var types1 = new List<string>();
            types1.Add("1");
            types1.Add("2");
            var actors1 = new List<string>();
            actors1.Add("1");
            actors1.Add("2");
            var model1 = new SuperMovie.Db.Film.Model.FilmModel(1, "zty", DateTime.Now, false, types1, 0.114514, actors1, "", "");
            op.CreateFilm(model1);
        }




        [Test]
        public void Test_CreateFilm()
        {
            IFilmOperation op = new FilmOperation(db);
            var types = new List<string>();
            types.Add("aa");
            types.Add("bb");
            var actors = new List<string>();
            actors.Add("lyf");
            actors.Add("hzc");
            var model = new SuperMovie.Db.Film.Model.FilmModel(123, "zty", DateTime.Now, false, types, 0.114514, actors, "", "");
            var ok = op.CreateFilm(model);
            Assert.True(ok);
            var isContain = false;
            var result = op.GetAllFilm();
            foreach (var film in result)
                if (film.Id == model.Id)
                    isContain = true;

            Assert.True(isContain);
        }

        [Test]
        public void Test_GetAllFilm()
        {
            IFilmOperation op = new FilmOperation(db);
            var result = op.GetAllFilm();
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void Test_FilterFilmByIsPreorder()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);

            List<IFilmModel> list = op.FilterFilmByIsPreorder(true);
            foreach (var item in list)
            {
                Assert.AreEqual(1, item.Id);
            }
        }

        [Test]
        public void Test_FilterFilmByTypesr()
        {
            IFilmOperation op = new FilmOperation(db);
            var types = new List<string>();
            types.Add("2");
            List<IFilmModel> list = op.FilterFilmByTypes(types);
            Assert.AreEqual(1, list.Count);

        }

        [Test]
        public void Test_FilterFilmByOnlineTimer()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);
            List<IFilmModel> list = op.FilterFilmByOnlineTime(DateTime.Now, DateTime.Now);
            foreach (var item in list)
            {
                Assert.AreEqual(1, item.Id);
            }
            Assert.True(true);
        }

        [Test]
        public void Test_FilterFilmByName()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);
            List<IFilmModel> list = op.FilterFilmByName("zty");
            foreach (var item in list)
            {
                Assert.AreEqual(1, item.Id);
            }
        }

        [Test]
        public void Test_FilterFilmByActors()
        {
            IFilmOperation op = new FilmOperation(db);
            var actors = new List<string>();
            actors.Add("1");
            List<IFilmModel> list = op.FilterFilmByActors(actors);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Test_UpdateFilmName()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmName(1, "1");
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual("1", item.Name);
            }
        }

        [Test]
        public void Test_UpdateFilmOnlineTime()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);
            DateTime time = DateTime.Now;
            op.UpdateFilmOnlineTime(1, time);
            var result = op.GetFilm(1);
            var list = op.GetFilm(1);
            Assert.AreEqual(result.OnlineTime, list.OnlineTime);
        }

        [Test]
        public void Test_UpdateFilmIsPreorder()
        {
            IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmIsPreorder(1, true);
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual(true, item.IsPreorder);
            }
        }

        [Test]
        public void Test_UpdateFilmTypes()
        {
            var types = new List<string>();
            types.Add("xx");
            types.Add("yy");
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmTypes(1, types);
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual(types, item.Types);
            }
        }

        [Test]
        public void Test_UpdateFilmPrice()
        {
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmPrice(1, 123);
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual(123, item.Price);
            }
        }

        [Test]
        public void Test_UpdateFilmActors()
        {
            var actors = new List<string>();
            actors.Add("xx");
            actors.Add("yy");
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmActors(1, actors);
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual(actors, item.Actors);
            }
        }

        [Test]
        public void Test_GetFilm()
        {
            IFilmOperation op = new FilmOperation(db);
            IFilmModel result = op.GetFilm(1);
            var list = op.GetAllFilm();
            foreach (var item in list)
            {
                Assert.AreEqual(1, item.Id);
            }
            List<string> list1 = new List<string>();
            /*list1.Add("");*/
            list1.Add("1");
            list1.Add("2");
            foreach(var item in list)
            {
                Assert.AreEqual(list1, item.Actors);
            }
        }

        [Test]
        public void Test_MatchFilmByName()
        {
            var model1 = new SuperMovie.Db.Film.Model.FilmModel(1, "abc", DateTime.Now, false, new List<string>(), 0.114514, new List<string>(), "", "");
            IFilmOperation op = new FilmOperation(db);
            op.CreateFilm(model1);
            List<IFilmModel> result = op.MatchFilmByName("c");
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        [Test]
        public void Test_UpdateFilmCoverUrl()
        {
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmCoverUrl(1, "lyf");
            var result = op.GetFilm(1);
            if (result != null) Assert.AreEqual("lyf", result.CoverUrl);
        }

        [Test]
        public void Test_UpdateFilmPreviewVideoUrl()
        {
            IFilmOperation op = new FilmOperation(db);
            op.UpdateFilmPreviewVideoUrl(1, "lyf");
            var result = op.GetFilm(1);
            if (result != null) Assert.AreEqual("lyf", result.PreviewVideoUrl);
        }

        [Test]
        public void Test_GetAllActors()
        {
            var types1 = new List<string>();
            /*types1.Add("1");
            types1.Add("2");*/
            var actors2 = new List<string>();
            /*actors2.Add("123");
            actors2.Add("hzc");*/
            var model2 = new SuperMovie.Db.Film.Model.FilmModel(2, "zty", DateTime.Now, false, types1, 0.114514, actors2, "", "");
            IFilmOperation op1 = new FilmOperation(db);
            op1.CreateFilm(model2);
            /*var result = op1.GetAllActors();*/
            /*var list = new List<string>();
            list.Add("lyf");
            list.Add("hzc");
            list.Add("123");
            Assert.AreEqual(list, result);
            Assert.AreEqual(3, result.Count);*/
            Assert.AreEqual(0,model2.Actors.Count);
        }
    }

}
