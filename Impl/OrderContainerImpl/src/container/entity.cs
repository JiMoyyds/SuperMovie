// Decompiled with JetBrains decompiler
// Type: SuperMovie.Container.Order.Entity.OrderEntity
// Assembly: OrderContainerImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6892D3A6-3714-4FB0-9364-31019630CFFE
// Assembly location: /home/thaumy/data/dev/repo/SuperMovieSDK/_build/OrderContainerImpl.dll

using DbMiddleware;
using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Entity;
using SuperMovie.Container.User.Provider;
using SuperMovie.Db.Film.Operation;
using SuperMovie.Db.Order.Operation;
using System;


#nullable enable
namespace SuperMovie.Container.Order.Entity
{
    public class OrderEntity : IOrderEntity
    {
        public IDatabase Db;
        public long id;
        public Func<IUserProvider> userProviderF;
        public IFilmProvider filmProvider;
        public Func<ICinemaProvider> cinemaProviderF;
        public Func<IScheduleProvider> scheduleProviderF;

        internal OrderEntity(
            long id,
            double payAmount,
            string seat,
            DateTime time,
            Func<IUserProvider> userProviderF,
            IFilmProvider filmProvider,
            Func<ICinemaProvider> cinemaProviderF,
            Func<IScheduleProvider> scheduleProviderF,
            IDatabase db)
        {
            this.userProviderF = userProviderF;
            this.filmProvider = filmProvider;
            this.cinemaProviderF = cinemaProviderF;
            this.scheduleProviderF = scheduleProviderF;
            this.id = id;
            this.Db = db;
        }

        public bool Drop() =>
            new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).DeleteOrder(this.id);

        public long Id => this.id;

        public IUserEntity? User
        {
            get
            {
                object id = this.Db.QueryForFirstValue(
                    "SELECT order_user_id FROM default_schema.order\r\n                              WHERE order_id = :order_id",
                    new (string, object)[1]
                    {
                        ("order_id", (object)this.id)
                    });
                return id == null ? (IUserEntity)null : this.userProviderF().GetUser((long)id) ?? (IUserEntity)null;
            }
            set
            {
                if (value == null)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderUserId(this.id,
                    value.Id);
            }
        }

        public double PayAmount
        {
            get
            {
                object obj = this.Db.QueryForFirstValue(
                    "SELECT order_pay_amount FROM  \r\n                              default_schema.order WHERE order_id = :id",
                    new (string, object)[1]
                    {
                        ("id", (object)this.id)
                    });
                return obj == null ? -1.0 : (double)obj;
            }
            set
            {
                if (value < 0.0)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderPayAmount(this.id,
                    value);
            }
        }

        public IFilmEntity? Film
        {
            get
            {
                object id = this.Db.QueryForFirstValue(
                    "SELECT order_film_id FROM default_schema.order\r\n                              WHERE order_id = :order_id",
                    new (string, object)[1]
                    {
                        ("order_id", (object)this.id)
                    });
                return id == null ? (IFilmEntity)null : this.filmProvider.GetFilm((long)id) ?? (IFilmEntity)null;
            }
            set
            {
                if (value == null)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderFilmId(this.id,
                    value.Id);
            }
        }

        public ICinemaEntity? Cinema
        {
            get
            {
                object id = this.Db.QueryForFirstValue(
                    "SELECT order_cinema_id FROM default_schema.order\r\n                              WHERE order_id = :order_id",
                    new (string, object)[1]
                    {
                        ("order_id", (object)this.id)
                    });
                return id == null
                    ? (ICinemaEntity)null
                    : this.cinemaProviderF().GetCinema((long)id) ?? (ICinemaEntity)null;
            }
            set
            {
                if (value == null)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderCinemaId(this.id,
                    value.Id);
            }
        }

        public IScheduleEntity? Schedule
        {
            get
            {
                object id = this.Db.QueryForFirstValue(
                    "SELECT order_schedule_id FROM default_schema.order\r\n                              WHERE order_id = :order_id",
                    new (string, object)[1]
                    {
                        ("order_id", (object)this.id)
                    });
                return id == null
                    ? (IScheduleEntity)null
                    : this.scheduleProviderF().GetSchedule((long)id) ?? (IScheduleEntity)null;
            }
            set
            {
                if (value == null)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderScheduleId(this.id,
                    value.Id);
            }
        }

        public string? Seat
        {
            get => (string)this.Db.QueryForFirstValue(
                "SELECT order_cinema_seat FROM \r\n                           default_schema.order WHERE order_id =:order_id",
                new (string, object)[1]
                {
                    ("order_id", (object)this.id)
                }) ?? "Invalid";
            set
            {
                if (value == null || value.Length == 0)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderCinemaSeat(this.id,
                    value);
            }
        }

        public DateTime? Time
        {
            get
            {
                object obj = this.Db.QueryForFirstValue(
                    "SELECT order_time FROM default_schema.order \r\n                           WHERE order_id= :id",
                    new (string, object)[1]
                    {
                        ("id", (object)this.id)
                    });
                return obj == null ? new DateTime?() : new DateTime?((DateTime)obj);
            }
            set
            {
                if (!value.HasValue)
                    return;
                new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderTime(this.id,
                    value.Value);
            }
        }

        public bool IsValid() =>
            new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).IsOrderIdValid(this.id);

        public string? Status
        {
            get => (string)this.Db.QueryForFirstValue(
                "SELECT order_status FROM default_schema.order \r\n                             WHERE order_id = :id",
                new (string, object)[1]
                {
                    ("id", (object)this.id)
                }) ?? "";
            set
            {
                switch (value)
                {
                    case "":
                        break;
                    case null:
                        break;
                    default:
                        new OrderOperation(this.Db, (IFilmOperation)new FilmOperation(this.Db)).UpdateOrderStatus(
                            this.id, value);
                        break;
                }
            }
        }
    }
}