// Decompiled with JetBrains decompiler
// Type: SuperMovie.Container.Order.Provider.OrderProvider
// Assembly: OrderContainerImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6892D3A6-3714-4FB0-9364-31019630CFFE
// Assembly location: /home/thaumy/data/dev/repo/SuperMovieSDK/_build/OrderContainerImpl.dll

using DbMiddleware;
using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Entity;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Order.Entity;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Entity;
using SuperMovie.Container.User.Provider;
using SuperMovie.Db.Film.Operation;
using SuperMovie.Db.Order.Model;
using SuperMovie.Db.Order.Operation;
using SuperMovie.Util;
using System;
using System.Collections.Generic;


#nullable enable
namespace SuperMovie.Container.Order.Provider
{
  public class OrderProvider : IOrderProvider
  {
    public IDatabase Db;
    public Func<IUserProvider> userProviderF;
    public IFilmProvider filmProvider;
    public Func<ICinemaProvider> cinemaProviderF;
    public Func<IScheduleProvider> scheduleProviderF;
    public IdGenerator Gen;
    public IFilmOperation filmOperation;

    public OrderProvider(
      Func<IUserProvider> userProviderF,
      IFilmProvider filmProvider,
      IFilmOperation filmOperation,
      Func<ICinemaProvider> cinemaProviderF,
      Func<IScheduleProvider> scheduleProviderF,
      IdGenerator gen,
      IDatabase db)
    {
      this.userProviderF = userProviderF;
      this.scheduleProviderF = scheduleProviderF;
      this.cinemaProviderF = cinemaProviderF;
      this.filmProvider = filmProvider;
      this.filmOperation = filmOperation;
      this.Gen = gen;
      this.Db = db;
    }

    public IOrderEntity? CreateOrder(
      IUserEntity user,
      double payAmount,
      IFilmEntity film,
      ICinemaEntity cinema,
      IScheduleEntity schedule,
      string seat)
    {
      long id = this.Gen.Next();
      OrderEntity orderEntity = new OrderEntity(id, payAmount, seat, DateTime.Now, this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
      return this.Db.Query("INSERT INTO default_schema.order\r\n             (order_id,order_user_id,order_pay_amount,order_time,order_film_id,\r\n             order_cinema_id,order_schedule_id,order_cinema_seat,order_status)\r\n             VALUES\r\n             (:order_id,:order_user_id,:order_pay_amount,:order_time,:order_film_id,\r\n             :order_cinema_id,:order_schedule_id,:order_cinema_seat,:order_status)", 1, new (string, object)[9]
      {
        ("order_id", (object) id),
        ("order_user_id", (object) user.Id),
        ("order_pay_amount", (object) payAmount),
        ("order_time", (object) DateTime.Now),
        ("order_film_id", (object) film.Id),
        ("order_cinema_id", (object) cinema.Id),
        ("order_schedule_id", (object) schedule.Id),
        ("order_cinema_seat", (object) seat),
        ("order_status", (object) "created")
      }) == 1 ? (IOrderEntity) orderEntity : (IOrderEntity) null;
    }

    public IOrderEntity? GetOrder(long id)
    {
      Dictionary<string, object> dictionary = this.Db.QueryForFirstRow("SELECT * FROM default_schema.order\r\n             WHERE order_id = :id", new (string, object)[1]
      {
        (nameof (id), (object) id)
      });
      return dictionary == null ? (IOrderEntity) null : (IOrderEntity) new OrderEntity((long) dictionary["order_id"], (double) dictionary["order_pay_amount"], (string) dictionary["order_cinema_seat"], (DateTime) dictionary["order_time"], this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
    }

    public List<IOrderEntity> GetAllOrder()
    {
      List<IOrderModel> allOrder1 = new OrderOperation(this.Db, this.filmOperation).GetAllOrder();
      List<IOrderEntity> allOrder2 = new List<IOrderEntity>();
      if (allOrder1.Count == 0)
        return allOrder2;
      foreach (IOrderModel orderModel in allOrder1)
      {
        OrderEntity orderEntity = new OrderEntity(orderModel.Id, orderModel.PayAmount, orderModel.CinemaSeat, orderModel.Time, this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
        allOrder2.Add((IOrderEntity) orderEntity);
      }
      return allOrder2;
    }

    public List<IOrderEntity> FilterOrderByFilm(IFilmEntity film)
    {
      List<IOrderEntity> orderEntityList = new List<IOrderEntity>();
      List<IOrderModel> orderModelList = new OrderOperation(this.Db, this.filmOperation).FilterOrderByFilmId(film.Id);
      if (orderModelList == null)
        return orderEntityList;
      foreach (IOrderModel orderModel in orderModelList)
      {
        OrderEntity orderEntity = new OrderEntity(orderModel.Id, orderModel.PayAmount, orderModel.CinemaSeat, orderModel.Time, this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
        orderEntityList.Add((IOrderEntity) orderEntity);
      }
      return orderEntityList;
    }

    public List<IOrderEntity> FilterOrderByFilmTypes(List<string> filmTypes)
    {
      List<IOrderEntity> orderEntityList = new List<IOrderEntity>();
      List<IOrderModel> orderModelList = new OrderOperation(this.Db, this.filmOperation).FilterOrderByFilmTypes(filmTypes);
      if (orderModelList.Count == 0)
        return orderEntityList;
      foreach (IOrderModel orderModel in orderModelList)
      {
        OrderEntity orderEntity = new OrderEntity(orderModel.Id, orderModel.PayAmount, orderModel.CinemaSeat, orderModel.Time, this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
        orderEntityList.Add((IOrderEntity) orderEntity);
      }
      return orderEntityList;
    }

    public List<IOrderEntity> FilterOrderByFilmActors(List<string> filmActors)
    {
      List<IOrderEntity> orderEntityList = new List<IOrderEntity>();
      List<IOrderModel> orderModelList = new OrderOperation(this.Db, this.filmOperation).FilterOrderByFilmActors(filmActors);
      if (orderModelList.Count == 0)
        return orderEntityList;
      foreach (IOrderModel orderModel in orderModelList)
      {
        OrderEntity orderEntity = new OrderEntity(orderModel.Id, orderModel.PayAmount, orderModel.CinemaSeat, orderModel.Time, this.userProviderF, this.filmProvider, this.cinemaProviderF, this.scheduleProviderF, this.Db);
        orderEntityList.Add((IOrderEntity) orderEntity);
      }
      return orderEntityList;
    }

    public List<(string filmActor, double boxOffice)> GetReleasedFilmActorBor()
    {
      List<IFilmEntity> allFilm = this.filmProvider.GetAllFilm();
      Dictionary<string, double> dictionary = new Dictionary<string, double>();
      foreach (IFilmEntity filmEntity in allFilm)
      {
        foreach (string actor in filmEntity.Actors)
        {
          if (!dictionary.ContainsKey(actor))
          {
            dictionary.Add(actor, 0.0);
            foreach (IOrderEntity orderByFilmActor in this.FilterOrderByFilmActors(new List<string>()
            {
              actor
            }))
            {
              if (orderByFilmActor.Status == "paid" && orderByFilmActor.Film != null)
                dictionary[actor] += orderByFilmActor.Film.Price;
            }
          }
        }
      }
      List<(string, double)> releasedFilmActorBor = new List<(string, double)>();
      foreach (KeyValuePair<string, double> keyValuePair in dictionary)
        releasedFilmActorBor.Add((keyValuePair.Key, keyValuePair.Value));
      releasedFilmActorBor.Sort((Comparison<(string, double)>) ((x, y) => (int) (y.Item2 - x.Item2)));
      return releasedFilmActorBor;
    }

    public List<(string filmName, double boxOffice)> GetReleasedFilmNameBor()
    {
      List<(string, double)> releasedFilmNameBor = new List<(string, double)>();
      List<Dictionary<string, object>> dictionaryList = this.Db.QueryForTable("SELECT film_id,film_name,film_price FROM \r\n                                  super_movie.default_schema.film", ((string, object)[]) null);
      if (dictionaryList == null)
        return releasedFilmNameBor;
      foreach (Dictionary<string, object> dictionary in dictionaryList)
      {
        double num1 = 0.0;
        object obj = this.Db.QueryForFirstValue("SELECT count(order_id)\r\n                                       FROM  default_schema.order \r\n                                       WHERE order_film_id = :film_id and order_status = 'paid'", new (string, object)[1]
        {
          ("film_id", dictionary["film_id"])
        });
        if (obj == null)
        {
          releasedFilmNameBor.Add(((string) dictionary["film_name"], num1));
        }
        else
        {
          double num2 = num1 + (double) dictionary["film_price"] * (double) (long) obj;
          releasedFilmNameBor.Add(((string) dictionary["film_name"], num2));
        }
      }
      releasedFilmNameBor.Sort((Comparison<(string, double)>) ((x, y) => (int) (y.Item2 - x.Item2)));
      return releasedFilmNameBor;
    }

    public List<(string filmType, double boxOffice)> GetReleasedFilmTypeBor()
    {
      List<IFilmEntity> allFilm = this.filmProvider.GetAllFilm();
      Dictionary<string, double> dictionary = new Dictionary<string, double>();
      foreach (IFilmEntity filmEntity in allFilm)
      {
        foreach (string type in filmEntity.Types)
        {
          if (!dictionary.ContainsKey(type))
          {
            dictionary.Add(type, 0.0);
            foreach (IOrderEntity filterOrderByFilmType in this.FilterOrderByFilmTypes(new List<string>()
            {
              type
            }))
            {
              if (filterOrderByFilmType.Status == "paid" && filterOrderByFilmType.Film != null)
                dictionary[type] += filterOrderByFilmType.Film.Price;
            }
          }
        }
      }
      List<(string, double)> releasedFilmTypeBor = new List<(string, double)>();
      foreach (KeyValuePair<string, double> keyValuePair in dictionary)
        releasedFilmTypeBor.Add((keyValuePair.Key, keyValuePair.Value));
      releasedFilmTypeBor.Sort((Comparison<(string, double)>) ((x, y) => (int) (y.Item2 - x.Item2)));
      return releasedFilmTypeBor;
    }
  }
}
