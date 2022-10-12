using NUnit.Framework;
using SuperMovie.Db.Order.Operation;
using SuperMovie.Db.Order.Model;
using DbMiddleware;
using DbMiddlewarePostgresImpl;
using SuperMovie.Db.Film.Operation;

namespace SuperMovie.Db.Order
{
    public class QueryForFirstColumn
    {
        IDatabase db = new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");

        [SetUp]

            /*       [Test]
             public void Test_CreateOrder()
                    {
                        OrderOperation b = new OrderOperation(db,null);
                        OrderModel model = new OrderModel(123456789, 156666, 34, DateTime.Now, 111, 222, 333, "第五排第三列","ok");
                        var result = b.CreateOrder(model);
                        Assert.IsTrue(result);
                    }*/
           
        /*     [Test]
             public void Test_DeleteOrder()
             {
                 OrderOperation b = new OrderOperation(db,null);
                 var result = b.DeleteOrder(111);
                 Assert.IsTrue(result);
             }*/

        [Test]
        public void Test_GetOrder()
        {
            OrderOperation b = new OrderOperation(db, null);
            var c = b.GetOrder(222);
            Assert.IsNotNull(c);
        }

        [Test]
        public void Test_GetAllOrder()
        {
            OrderOperation b = new OrderOperation(db, null);
            var result = b.GetAllOrder();
            foreach (var item in result)
            {
                Console.WriteLine(item.Id);
            }
        }

        [Test]
        public void Test_FilterOrderByUserId()
        {
            IFilmOperation filmOperation = new FilmOperation(db);
            OrderOperation b = new OrderOperation(db, filmOperation);
            var result = b.FilterOrderByUserId(111);
            foreach (var item in result)
            {
                Console.WriteLine(item.Id);
            }
        }

        [Test]//测试不存在时
        public void Test_FilterOrderByUserId2()
        {
            OrderOperation b = new OrderOperation(db, null);
            var result = b.FilterOrderByUserId(1111);
            var count = result.Count;
            Console.WriteLine(count);
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_FilterOrderByFilmTypes()
        {
            List<string> types = new List<string>();
            types.Add("jhjk1");
            types.Add("wer");
            IFilmOperation filmOperation = new FilmOperation(db);
            OrderOperation op = new OrderOperation(db, filmOperation);

            var result = op.FilterOrderByFilmTypes(types);
            foreach(var item in result)
            {
                Console.WriteLine(item.Id);
            }
      
        }

        [Test]
        public void Test_FilterOrderByFilmActors()
        {
            IFilmOperation filmOperation = new FilmOperation(db);
            OrderOperation b = new OrderOperation(db, filmOperation);
            List<string> list = new List<string>();
            list.Add("www");
            list.Add("wj");
            var result = b.FilterOrderByFilmActors(list);
         Console.WriteLine(result.Count);
        }

        [Test]
        public void Test_IsOrderIdValid()
        {
            OrderOperation b = new OrderOperation(db, null);
            var result = b.IsOrderIdValid(333);
            Assert.IsTrue(result);
        }
        /*
                [Test]
                public void Test_UpdateOrderUserId()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderUserId(444, 188);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderPayAmount()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderPayAmount(555, 88);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderTime()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderTime(666, DateTime.Now);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderFilmId()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderFilmId(777, 333333);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderCinemaId()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderCinemaId(888, 6666);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderScheduleId()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderScheduleId(999, 777777);
                    Assert.IsTrue(result);
                }

                [Test]
                public void Test_UpdateOrderCinemaSeat()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var c = b.UpdateOrderCinemaSeat(123, "第一排第2列");
                    Assert.True(c);
                }
                [Test]
                public void Test_UpdateOrderStatus()
                {
                    OrderOperation b = new OrderOperation(db, null);
                    var result = b.UpdateOrderStatus(111, "No");
                    Assert.IsTrue(result);
                }*/

    }
}
