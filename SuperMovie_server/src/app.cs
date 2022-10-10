using System;
using System.Threading.Tasks;
using DbMiddlewarePostgresImpl;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Container.Schedule.Entity;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Provider;
using SuperMovie.Container.Vip.Provider;
using WebSocketSharp.Server;
using SuperMovie.Server.Api;
using SuperMovie.Server.Api.Cinema;
using SuperMovie.Server.Api.Film;
using SuperMovie.Server.Api.Order;
using SuperMovie.Server.Api.Schedule;
using SuperMovie.Server.Api.Statistics;
using SuperMovie.Server.Api.User;
using SuperMovie.Util;

Host.CreateDefaultBuilder()
    .ConfigureServices(
        (ctx, ss) => { ss.AddHostedService<Worker>(); }
    ).Build().Run();

public class Worker : BackgroundService
{
    public Worker() : base()
    {
    }

    protected override Task ExecuteAsync(CancellationToken ct)
    {
        var wsServer = new WebSocketServer("ws://localhost:11451");
        var cinemaGen = new IdGenerator(ActionType.Cinema);
        var filmGen = new IdGenerator(ActionType.Film);
        var scheduleGen = new IdGenerator(ActionType.Schedule);
        var db = new PostgresDatabase
        (
            "postgres",
            "65a1561425f744e2b541303f628963f8",
            "localhost",
            5432,
            "super_movie"
        );

        ICinemaProvider cinemaProvider = null;
        IFilmProvider filmProvider = null;
        IOrderProvider orderProvider = null;
        IScheduleProvider scheduleProvider = null;
        IUserProvider userProvider = null;
        IVipProvider vipProvider = null;

        var cinemaProviderF = () => cinemaProvider;
        var filmProviderF = () => filmProvider;
        var orderProviderF = () => orderProvider;
        var scheduleProviderF = () => scheduleProvider;
        var userProviderF = () => userProvider;
        var vipProviderF = () => vipProvider;

        cinemaProvider = new CinemaProvider(scheduleProviderF, cinemaGen, db);
        filmProvider = new FilmProvider(filmGen, db);
        orderProvider = null;
        scheduleProvider = new ScheduleProvider(cinemaProviderF, filmProvider, scheduleGen, db);
        userProvider = null;
        vipProvider = null;

//Cinema
        wsServer.AddWebSocketService<AddCinema>
        ("/add_cinema",
            handler => handler.Set(cinemaProviderF()));
        wsServer.AddWebSocketService<DeleteCinema>
        ("/delete_cinema",
            handler => handler.Set(cinemaProviderF()));
        wsServer.AddWebSocketService<GetAllCinema>
        ("/get_all_cinema",
            handler => handler.Set(cinemaProviderF()));

//Film
        wsServer.AddWebSocketService<AddFilm>
        ("/add_film",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<DeleteFilm>
        ("/delete_film",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<GetAllFilm>
        ("/get_all_film",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<GetAllFilmType>
        ("/get_all_film_type",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<GetAllFilmOnlineTime>
        ("/get_all_film_online_time",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<GetFilm>
        ("/get_film",
            handler => handler.Set(filmProviderF()));
        wsServer.AddWebSocketService<SearchFilm>
        ("/search_film",
            handler => handler
                .Set(
                    filmProviderF(),
                    scheduleProviderF()
                )
        );
        wsServer.AddWebSocketService<UpdateFilm>
        ("/update_film",
            handler => handler.Set(filmProviderF()));

//Order
        wsServer.AddWebSocketService<CreateOrder>
        ("/create_order",
            handler => handler
                .Set(
                    userProviderF(),
                    filmProviderF(),
                    cinemaProviderF(),
                    scheduleProviderF(),
                    orderProviderF()
                )
        );
        wsServer.AddWebSocketService<GetAllOrder>
        ("/get_all_order",
            handler => handler.Set(orderProviderF()));
        wsServer.AddWebSocketService<GetAllOrderWithUserId>
        ("/get_all_order_with_user_id",
            handler => handler.Set(userProviderF()));
        wsServer.AddWebSocketService<IsOrderIdValid>
        ("/is_order_id_valid",
            handler => handler.Set(orderProviderF()));
        wsServer.AddWebSocketService<RefundOrder>
        ("/refund_order",
            handler => handler.Set(orderProviderF()));

//Schedule
        wsServer.AddWebSocketService<AddSchedule>
        ("/add_schedule",
            handler => handler
                .Set(
                    scheduleProviderF(),
                    cinemaProviderF(),
                    filmProviderF()
                )
        );
        wsServer.AddWebSocketService<DeleteSchedule>
        ("/delete_schedule",
            handler => handler.Set(scheduleProviderF()));
        wsServer.AddWebSocketService<GetAllScheduleByCinemaId>
        ("/get_all_schedule_by_cinema_id",
            handler => handler.Set(scheduleProviderF(), cinemaProviderF()));

//Statistics
        wsServer.AddWebSocketService<GetReleasedFilmActorBor>
        ("/get_released_film_actor_bor",
            handler => handler.Set(orderProviderF()));
        wsServer.AddWebSocketService<GetReleasedFilmNameBor>
        ("/get_released_film_name_bor",
            handler => handler.Set(orderProviderF()));
        wsServer.AddWebSocketService<GetReleasedFilmTypeBor>
        ("/get_released_film_type_bor",
            handler => handler.Set(orderProviderF()));

//User
        wsServer.AddWebSocketService<AddUser>
        ("/add_user",
            handler => handler
                .Set(
                    userProviderF(),
                    vipProviderF()
                )
        );
        wsServer.AddWebSocketService<IsUserIdMatchPwd>
        ("/is_user_id_match_pwd",
            handler => handler.Set(userProviderF()));
        wsServer.AddWebSocketService<RefundUserVip>
        ("/refund_user_vip",
            handler => handler.Set(userProviderF()));
        wsServer.AddWebSocketService<UpgradeUserVip>
        ("/upgrade_user_vip",
            handler => handler
                .Set(
                    userProviderF(),
                    vipProviderF()
                )
        );

        return Task.Run(() => { wsServer.Start(); });
    }
}