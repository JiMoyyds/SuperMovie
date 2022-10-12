// Decompiled with JetBrains decompiler
// Type: SuperMovie.Container.Order.test
// Assembly: OrderContainerImpl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6892D3A6-3714-4FB0-9364-31019630CFFE
// Assembly location: /home/thaumy/data/dev/repo/SuperMovieSDK/_build/OrderContainerImpl.dll

using DbMiddleware;
using DbMiddlewarePostgresImpl;
using NUnit.Framework;
using SuperMovie.Container.Cinema.Entity;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Provider;
using SuperMovie.Db.Film.Operation;
using SuperMovie.Db.Order.Model;
using SuperMovie.Db.Order.Operation;
using SuperMovie.Util;
using System;
using System.Collections.Generic;


#nullable enable
namespace SuperMovie.Container.Order
{
  public class test
  {
    public IDatabase Db = (IDatabase) new PostgresDatabase("postgres", "123456", "localhost", 5432, "super_movie");

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_CreateOrder()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> userProviderF = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> orderProviderF = (Func<OrderProvider>) (() => orderProvider);
      Func<ScheduleProvider> scheduleProviderF = (Func<ScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) func, (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) scheduleProviderF, Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      OrderProvider orderProvider1 = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen3, this.Db);
      if (scheduleProvider.GetSchedule(8L) != null)
      {
        Console.WriteLine(orderProvider.CreateOrder(userProvider.GetUser(111L), 66.0, filmProvider.GetFilm(888L), cinemaProvider.GetCinema(6666L), scheduleProvider.GetSchedule(8L), "第五排第五列").Id);
      }
      else
      {
        ICinemaEntity cinema = cinemaProvider.GetCinema(6666L);
        if (cinema == null)
        {
          Console.WriteLine(cinema.Id);
        }
        else
        {
          Console.WriteLine(userProviderF().GetUser(111L).Id);
          Console.WriteLine(filmProvider.GetFilm(888L).Id);
          Console.WriteLine(func().GetCinema(6666L).Id);
          Console.WriteLine(scheduleProviderF().GetSchedule(8L).Id);
        }
      }
    }

    [Test]
    public void Test_GetOrder()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func1 = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> func2 = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> func3 = (Func<OrderProvider>) (() => orderProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) (() => (ICinemaProvider) cinemaProvider), (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) (() => (IScheduleProvider) scheduleProvider), Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) (() => (IUserProvider) userProvider), (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) (() => (ICinemaProvider) cinemaProvider), (Func<IScheduleProvider>) (() => (IScheduleProvider) scheduleProvider), gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) (() => (IOrderProvider) orderProvider), this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      Assert.IsNull((object) new OrderProvider((Func<IUserProvider>) (() => (IUserProvider) userProvider), (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) (() => (ICinemaProvider) cinemaProvider), (Func<IScheduleProvider>) (() => (IScheduleProvider) scheduleProvider), gen3, this.Db).GetOrder(gen3.Next()));
    }

    [Test]
    public void Test_GetOrder2()
    {
      ICinemaProvider cinemaProvider = (ICinemaProvider) null;
      IScheduleProvider scheduleProvider = (IScheduleProvider) null;
      IFilmProvider filmProvider = (IFilmProvider) new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      IUserProvider userProvider = (IUserProvider) null;
      IOrderProvider orderProvider = (IOrderProvider) null;
      Func<ICinemaProvider> func = (Func<ICinemaProvider>) (() => cinemaProvider);
      Func<IUserProvider> userProviderF = (Func<IUserProvider>) (() => userProvider);
      Func<IOrderProvider> orderProviderF = (Func<IOrderProvider>) (() => orderProvider);
      Func<IScheduleProvider> scheduleProviderF = (Func<IScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = (IScheduleProvider) new ScheduleProvider(func, filmProvider, gen2, this.Db);
      cinemaProvider = (ICinemaProvider) new CinemaProvider(scheduleProviderF, Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = (IOrderProvider) new OrderProvider(userProviderF, filmProvider, filmOperation, func, scheduleProviderF, gen1, this.Db);
      userProvider = (IUserProvider) new UserProvider(orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      IOrderProvider orderProvider1 = (IOrderProvider) new OrderProvider(userProviderF, filmProvider, filmOperation, func, scheduleProviderF, gen3, this.Db);
      Assert.IsNotNull((object) orderProvider.GetOrder(333L));
    }

    [Test]
    public void Test_GetAllOrder()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> userProviderF = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> orderProviderF = (Func<OrderProvider>) (() => orderProvider);
      Func<ScheduleProvider> scheduleProviderF = (Func<ScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) func, (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) scheduleProviderF, Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      OrderProvider orderProvider1 = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen3, this.Db);
      Assert.AreNotEqual((object) 0, (object) orderProvider.GetAllOrder().Count);
    }

    [Test]
    public void Test_FilterOrderByFilm() => Assert.AreNotEqual((object) 0, (object) new OrderOperation(this.Db, (IFilmOperation) new FilmOperation(this.Db)).FilterOrderByFilmId(new FilmProvider((IdGenerator) null, this.Db).GetFilm(111L).Id).Count);

    [Test]
    public void Test_FilterOrderByFilmTypes()
    {
      IFilmOperation filmOp = (IFilmOperation) new FilmOperation(this.Db);
      foreach (IOrderModel filterOrderByFilmType in new OrderOperation(this.Db, filmOp).FilterOrderByFilmTypes(new List<string>()
      {
        "jhjk1"
      }))
        Console.WriteLine(filterOrderByFilmType.Id);
    }

    [Test]
    public void Test_FilterOrderByFilmActors()
    {
      IFilmOperation filmOp = (IFilmOperation) new FilmOperation(this.Db);
      foreach (IOrderModel orderByFilmActor in new OrderOperation(this.Db, filmOp).FilterOrderByFilmActors(new List<string>()
      {
        "111"
      }))
        Console.WriteLine(orderByFilmActor.Id);
    }

    [Test]
    public void Test_GetReleasedFilmActorBor()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> userProviderF = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> orderProviderF = (Func<OrderProvider>) (() => orderProvider);
      Func<ScheduleProvider> scheduleProviderF = (Func<ScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) func, (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) scheduleProviderF, Gen, this.Db);
      FilmOperation filmOperation = new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, (IFilmOperation) filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      foreach ((string filmActor, double boxOffice) tuple in new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, (IFilmOperation) filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen3, this.Db).GetReleasedFilmActorBor())
        Console.WriteLine("演员: " + tuple.filmActor + "票房: " + tuple.boxOffice.ToString());
    }

    [Test]
    public void Test_GetReleasedFilmNameBor()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> userProviderF = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> orderProviderF = (Func<OrderProvider>) (() => orderProvider);
      Func<ScheduleProvider> scheduleProviderF = (Func<ScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) func, (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) scheduleProviderF, Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      foreach ((string filmName, double boxOffice) tuple in new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen3, this.Db).GetReleasedFilmNameBor())
        Console.WriteLine("电影名:  " + tuple.filmName + "票房： " + tuple.boxOffice.ToString());
    }

    [Test]
    public void Test_GetReleasedFilmTypeBor()
    {
      CinemaProvider cinemaProvider = (CinemaProvider) null;
      ScheduleProvider scheduleProvider = (ScheduleProvider) null;
      FilmProvider filmProvider = new FilmProvider(new IdGenerator(ActionType.Film), this.Db);
      UserProvider userProvider = (UserProvider) null;
      OrderProvider orderProvider = (OrderProvider) null;
      Func<CinemaProvider> func = (Func<CinemaProvider>) (() => cinemaProvider);
      Func<UserProvider> userProviderF = (Func<UserProvider>) (() => userProvider);
      Func<OrderProvider> orderProviderF = (Func<OrderProvider>) (() => orderProvider);
      Func<ScheduleProvider> scheduleProviderF = (Func<ScheduleProvider>) (() => scheduleProvider);
      IdGenerator gen1 = new IdGenerator(ActionType.Order);
      IdGenerator Gen = new IdGenerator(ActionType.Cinema);
      IdGenerator gen2 = new IdGenerator(ActionType.Schedule);
      scheduleProvider = new ScheduleProvider((Func<ICinemaProvider>) func, (IFilmProvider) filmProvider, gen2, this.Db);
      cinemaProvider = new CinemaProvider((Func<IScheduleProvider>) scheduleProviderF, Gen, this.Db);
      IFilmOperation filmOperation = (IFilmOperation) new FilmOperation(this.Db);
      orderProvider = new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen1, this.Db);
      userProvider = new UserProvider((Func<IOrderProvider>) orderProviderF, this.Db);
      IdGenerator gen3 = new IdGenerator(ActionType.Order);
      foreach ((string filmType, double boxOffice) tuple in new OrderProvider((Func<IUserProvider>) userProviderF, (IFilmProvider) filmProvider, filmOperation, (Func<ICinemaProvider>) func, (Func<IScheduleProvider>) scheduleProviderF, gen3, this.Db).GetReleasedFilmTypeBor())
        Console.WriteLine("电影类型：" + tuple.filmType + "票房：" + tuple.boxOffice.ToString());
    }
  }
}
