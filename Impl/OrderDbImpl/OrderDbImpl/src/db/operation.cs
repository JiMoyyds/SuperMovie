using DbMiddleware;
using DbMiddlewarePostgresImpl;
using SuperMovie.Db.Order.Model;
using SuperMovie.Db.Film.Operation;

namespace SuperMovie.Db.Order.Operation
{
    public class OrderOperation : IOrderOperation
    {
        IDatabase Db;
        private IFilmOperation _filmOp;
        public OrderOperation(IDatabase db, IFilmOperation filmOp)
        {
            Db = db;
            _filmOp = filmOp;
        }

        public bool CreateOrder(IOrderModel model)
        {
            var flag = Db.Query(@$"INSERT INTO default_schema.order
             (order_id,order_user_id,order_pay_amount,order_time,order_film_id,
             order_cinema_id,order_schedule_id,order_cinema_seat,order_status)
             VALUES
             (:order_id,:order_user_id,:order_pay_amount,:order_time,:order_film_id,
             :order_cinema_id,:order_schedule_id,:order_cinema_seat,:order_status)", 1,
             new[] {
                 ("order_id",(object)model.Id),
                 ("order_user_id",(object)model.UserId),
                 ("order_pay_amount",(object)model.PayAmount),
                 ("order_time",(object)model.Time),
                 ("order_film_id",(object)model.FilmId),
                 ("order_cinema_id",(object)model.CinemaId),
                 ("order_schedule_id",(object)model.ScheduleId),
                 ("order_cinema_seat",(object)model.CinemaSeat),
                 ("order_status",(object)model.Status)
             });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool DeleteOrder(long orderId)
        {
            var flag = Db.Query(@$"DELETE FROM default_schema.order WHERE order_id = :orderId", 1,
                new[]
                {
                    ("orderId",(object)orderId)
                }
                );
            if (flag == 1)
                return true;
            else return false;

        }
        public IOrderModel? GetOrder(long orderId)
        {

            var result = Db.QueryForFirstRow($@"SELECT *FROM default_schema.order WHERE order_id = :orderId",
                new[]
                {
                    ("orderId", (object)orderId)
                });
            if (result == null)
                return null;
            else
            {
                var model = new OrderModel(
                       (long)result["order_id"],
                    (long)result["order_user_id"],
                    (double)result["order_pay_amount"],
                    (DateTime)result["order_time"],
                    (long)result["order_film_id"],
                    (long)result["order_cinema_id"],
                    (long)result["order_schedule_id"],
                    (string)result["order_cinema_seat"],
                    (string)result["order_status"]
                    );
                return model;
            }
        }
        public List<IOrderModel> GetAllOrder()
        {
            List<IOrderModel> List = new List<IOrderModel>();
            var result = Db.QueryForTable("SELECT * FROM default_schema.order", null);
            foreach (var item in result)
            {
                var model = new OrderModel(
                    (long)item["order_id"],
                    (long)item["order_user_id"],
                    (double)item["order_pay_amount"],
                    (DateTime)item["order_time"],
                    (long)item["order_film_id"],
                    (long)item["order_cinema_id"],
                    (long)item["order_schedule_id"],
                    (string)item["order_cinema_seat"],
                    (string)item["order_status"]
                    );
                List.Add(model);
            }
            return List;
        }
        public List<IOrderModel> FilterOrderByUserId(long userId)
        {
            List<IOrderModel> List = new List<IOrderModel>();
            var result = Db.QueryForTable($@"SELECT * FROM default_schema.order WHERE order_user_id = :userId",
                new[]
                {
                    ("userId",(object)userId)
                });
            foreach (var item in result)
            {
                var model = new OrderModel(
                       (long)item["order_id"],
                    (long)item["order_user_id"],
                    (double)item["order_pay_amount"],
                    (DateTime)item["order_time"],
                    (long)item["order_film_id"],
                    (long)item["order_cinema_id"],
                    (long)item["order_schedule_id"],
                    (string)item["order_cinema_seat"],
                    (string)item["order_status"]
                    );
                List.Add(model);
            }
            return List;

        }
        public List<IOrderModel> FilterOrderByFilmId(long filmId)
        {
            List<IOrderModel> List = new List<IOrderModel>();
            var result = Db.QueryForTable($@"SELECT * FROM default_schema.order WHERE order_film_id = :filmId",
                new[]
                {
                    ("filmId",(object)filmId)
                });
            foreach (var item in result)
            {
                var model = new OrderModel(
                       (long)item["order_id"],
                    (long)item["order_user_id"],
                    (double)item["order_pay_amount"],
                    (DateTime)item["order_time"],
                    (long)item["order_film_id"],
                    (long)item["order_cinema_id"],
                    (long)item["order_schedule_id"],
                    (string)item["order_cinema_seat"],
                    (string)item["order_status"]
                    );
                List.Add(model);
            }
            return List;
        }
        public List<IOrderModel> FilterOrderByFilmTypes(List<string> filmTypes)
        {
            List<IOrderModel> List = new List<IOrderModel>();
            var filmmodel = _filmOp.FilterFilmByTypes(filmTypes);
            if (filmmodel.Count == 0)
                return List;
            else
            {
                foreach (var item in filmmodel)
                {
                    var filmId = item.Id;

                    var orders = Db.QueryForTable($@"SELECT * FROM default_schema.order WHERE order_film_id = :filmId",
                        new[]
                        {
                    ("filmId",(object)filmId)
                        });
                    foreach (var order in orders)
                    {
                        var model = new OrderModel(
                               (long)order["order_id"],
                            (long)order["order_user_id"],
                            (double)order["order_pay_amount"],
                            (DateTime)order["order_time"],
                            (long)order["order_film_id"],
                            (long)order["order_cinema_id"],
                            (long)order["order_schedule_id"],
                            (string)order["order_cinema_seat"],
                            (string)order["order_status"]
                            );
                        List.Add(model);
                    }


                }

                return List;
            }
        }
        public List<IOrderModel> FilterOrderByFilmActors(List<string> filmActors)
        {
            List<IOrderModel> List = new List<IOrderModel>();
            var films = _filmOp.FilterFilmByActors(filmActors);
            if (films.Count == 0)
                return List;
            else
            {
                foreach (var item in films)
                {
                    var filmId = item.Id;

                    var result = Db.QueryForTable($@"SELECT * FROM default_schema.order WHERE order_film_id = :filmId",
                        new[]
                        {
                    ("filmId",(object)filmId)
                        });
                    foreach (var row in result)
                    {
                        var model = new OrderModel(
                               (long)row["order_id"],
                            (long)row["order_user_id"],
                            (double)row["order_pay_amount"],
                            (DateTime)row["order_time"],
                            (long)row["order_film_id"],
                            (long)row["order_cinema_id"],
                            (long)row["order_schedule_id"],
                            (string)row["order_cinema_seat"],
                            (string)row["order_status"]

                            );
                        List.Add(model);
                    }
                }
                return List;
            }
        }
        public bool IsOrderIdValid(long orderId)
        {
            var result = Db.QueryForFirstValue($@"SELECT * FROM default_schema.order WHERE order_id = :orderId",
                new[]
                {
                    ("orderId",(object)orderId)
                });

            if (result == null)
                return false;
            else return true;

        }
        public bool UpdateOrderUserId(long orderId, long newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_user_id = :newValue
                               WHERE order_id = :orderId", 1,
                               new[]
                               { 
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                               });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderPayAmount(long orderId, double newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_pay_amount = :newValue
                               WHERE order_id = :orderId", 1,
                                new[]
                                {
                                   ("newValue",(object)newValue),
                                   ("orderId",(object)orderId)
                                });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderTime(long orderId, DateTime newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_time = :newValue
                               WHERE order_id = :orderId", 1,
                             new[]
                             {
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                             });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderFilmId(long orderId, long newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_film_id = :newValue
                               WHERE order_id = :orderId", 1,
                            new[]
                            {
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                            });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderCinemaId(long orderId, long newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_cinema_id = :newValue
                               WHERE order_id = :orderId", 1,
                           new[]
                           {
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                           });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderScheduleId(long orderId, long newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_schedule_id = :newValue
                               WHERE order_id = :orderId", 1,
                            new[]
                            {
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                            });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderCinemaSeat(long orderId, string newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_cinema_seat = :newValue
                               WHERE order_id = :orderId", 1,
                            new[]
                            {
                                   ("newValue",(object)newValue),
                                   ("OrderId",(object)orderId)
                            });
            if (flag == 1)
                return true;
            else return false;
        }
        public bool UpdateOrderStatus(long orderId, string newValue)
        {
            var flag = Db.Query(@$"UPDATE default_schema.order SET order_status = :newValue
                               WHERE order_id = :orderId", 1,
                               new[]
                               {
                                   ("newValue",(object)newValue),
                                   ("orderId",(object)orderId)
                               });
            if (flag == 1)
                return true;
            else
                return false;
        }

    }
}
