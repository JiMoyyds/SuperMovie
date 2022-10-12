using DbMiddleware;
using DbMiddlewarePostgresImpl;
using NUnit.Framework;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Util;
namespace test
{
    internal class test
    {
        IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test_CreateFilm()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, true, 0.1);
            Assert.IsNotNull(result1);
        }

        [Test]
        public void Test_GetFilm()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, true, 0.1);
            Assert.IsNotNull(result1);
            IFilmEntity result2 = op.GetFilm(result1.Id);
            Assert.IsNotNull(result2);
            Assert.AreEqual(result1.Id, result2.Id);

        }

        [Test]
        public void Test_GetAllFilm()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, true, 0.1);
            IFilmEntity result2 = op.CreateFilm("2", DateTime.Now, true, 0.4);
            result1.AddType("1"); result1.AddType("2");
            result2.AddType("3");result2.AddType("4");
            List<string> list1 = new List<string>();
            list1.Add("1"); list1.Add("2");
            Assert.AreEqual(list1, result1.Types);
        }

        [Test]
        public void Test_FilterFilmByIsPreorder()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            var result3 = op.FilterFilmByIsPreorder(false);
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id == result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_AddType()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity? result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            if (result1 != null)
            {
                IFilmEntity op1 = new FilmEntity(result1.Id, db);
                op1.AddType("123");
                List<string> list = new List<string>();
                list.Add("123");
                op1.AddType("456");
                list.Add("456");
                IFilmEntity result = op.GetFilm(result1.Id);
                Assert.AreEqual(list, result.Types);
            }

        }

        [Test]
        public void Test_RemoveType()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            IFilmEntity op1 = new FilmEntity(result1.Id, db);
            op1.AddType("123");
            op1.AddType("456");
            List<string> list = new List<string>();
            list.Add("456");
            op1.RemoveType("123");
            IFilmEntity result2 = op.GetFilm(result1.Id);
            Assert.AreEqual(list, result2.Types);
        }

        [Test]
        public void Test_ClearType()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            IFilmEntity op1 = new FilmEntity(result1.Id, db);
            op1.AddType("123");
            op1.AddType("456");
            op1.ClearType();
            IFilmEntity result2 = op.GetFilm(result1.Id);
        }

        [Test]
        public void Test_AddActor()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity? result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            if (result1 != null)
            {
                IFilmEntity op1 = new FilmEntity(result1.Id, db);
                op1.AddActor("123");
                List<string> list = new List<string>();
                list.Add("123");
                op1.AddActor("456");
                list.Add("456");
                IFilmEntity result = op.GetFilm(result1.Id);
            }
        }

        [Test]
        public void Test_RemoveActor()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            IFilmEntity op1 = new FilmEntity(result1.Id, db);
            op1.AddActor("123");
            op1.AddActor("456");
            List<string> list = new List<string>();
            list.Add("123");
            op1.RemoveActor("456");
            IFilmEntity result2 = op.GetFilm(result1.Id);
            Assert.AreEqual(list, result2.Actors);
        }

        [Test]
        public void Test_ClearActor()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            IFilmEntity op1 = new FilmEntity(result1.Id, db);
            op1.AddActor("123");
            op1.AddActor("456");
            op1.ClearActor();
            IFilmEntity result2 = op.GetFilm(result1.Id);
        }

        [Test]
        public void Test_FilterFilmByTypes()
        {
            db.Query("DELETE FROM default_schema.film", -1, null);
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddType("567");
            result1.AddActor("1111");
            List<string> list = new List<string>();
            list.Add("1234");
            var result3 = op.FilterFilmByTypes(list);
            Assert.AreEqual(1, result3.Count);
        }

        [Test]
        public void Test_FilterFilmByOnlineTime()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time;
            IFilmEntity result1 = op.CreateFilm("1", time = DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("1111");
            List<string> list = new List<string>();
            list.Add("1234");
            var result3 = op.FilterFilmByOnlineTime(time, DateTime.Now);
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id == result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_FilterFilmByName()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time;
            IFilmEntity result1 = op.CreateFilm("123", time = DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("1111");
            List<string> list = new List<string>();
            list.Add("1234");
            var result3 = op.FilterFilmByName("123");
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id == result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_MatchFilmByName()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time;
            IFilmEntity result1 = op.CreateFilm("1234", time = DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("1111");
            List<string> list = new List<string>();
            list.Add("1234");
            var result3 = op.MatchFilmByName("234");
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id == result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_FilterFilmByActors()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("11112");
            List<string> list = new List<string>();
            list.Add("11112");
            var result3 = op.FilterFilmByActors(list);
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id == result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_Drop()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("11112");
            List<string> list = new List<string>();
            list.Add("11112");
            result1.Drop();
            var result3 = op.GetAllFilm();
            var judge = false;
            foreach (IFilmEntity result in result3)
            {
                if (result1.Id != result.Id)
                {
                    judge = true;
                }
            }
            Assert.True(judge);
        }

        [Test]
        public void Test_IsValid()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            result1.AddType("1234");
            result1.AddActor("111123");
            List<string> list = new List<string>();
            list.Add("11112");
            bool judge = result1.IsValid();
            Assert.True(judge);
        }

        [Test]
        public void Test_Name()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            IFilmEntity result1 = op.CreateFilm("1", DateTime.Now, false, 0.1);
            Assert.AreEqual(result1.Name, "1");
        }

        [Test]
        public void Test_OnlineTime()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.OnlineTime, result1.OnlineTime);
        }

        [Test]
        public void Test_IsPreorder()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.IsPreorder, result1.IsPreorder);
        }

        [Test]
        public void Test_Types()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            result1.AddType("123");
            result1.AddType("456");
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.Types, result1.Types);
        }

        [Test]
        public void Test_Price()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.Price, result1.Price);
        }

        [Test]
        public void Test_Actors()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            result1.AddActor("123");
            result1.AddActor("456");
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.Actors, result1.Actors);
        }

        [Test]
        public void Test_CoverUrl()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            result1.CoverUrl = ("123");
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.CoverUrl, result1.CoverUrl);
        }

        [Test]
        public void Test_PreviewVideoUrl()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("456", time, false, 0.1);
            result1.PreviewVideoUrl = ("123");
            var result = op.GetFilm(result1.Id);
            Assert.AreEqual(result.PreviewVideoUrl, result1.PreviewVideoUrl);
        }

        [Test]
        public void Test_GetAllFilmType()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            DateTime time = DateTime.Now;
            IFilmEntity result1 = op.CreateFilm("1", time, false, 0.1);
            //result1.AddType("");
            /*result1.AddType("123");
            result1.AddType("456");*/
            //IFilmEntity result2 = op.CreateFilm("2", time, false, 0.1);
            /*result2.AddType("123");
            result2.AddType("1");*/
            //result2.AddType("");
            /*List<string> list = new List<string>();
            list.Add("123");
            list.Add("456");
            list.Add("1");
            Assert.AreEqual(list, result3);*/
            Assert.AreEqual(0,result1.Types.Count);
        }

        [Test]
        public void Test_GetAllFilmOnlineTime()
        {
            var gen = new IdGenerator(ActionType.Film);
            IFilmProvider op = new FilmProvider(gen, db);
            var result3 = op.GetAllFilmOnlineTime();
            Assert.AreEqual(3, result3.Count);

        }
    }
}