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
using SuperMovie.Db.Film.Operation;
using WebSocketSharp.Server;
using SuperMovie.Server.Api;
using SuperMovie.Server.Api.Cinema;
using SuperMovie.Server.Api.Film;
using SuperMovie.Server.Api.Order;
using SuperMovie.Server.Api.Schedule;
using SuperMovie.Server.Api.Statistics;
using SuperMovie.Server.Api.User;
using SuperMovie.Util;
using AlipayF2F;

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
        var f2fCLient = new F2FClient(
            "2021000121674567",
            "MIIEowIBAAKCAQEAuXnPMvYnNx1LhYIahlu0xRQNtaypfVzJtZnF4WVrI6Dq5quVhZgbW5GiPstMGoMkGSLDtEtPxVxJU0LXlNE4bNscuCjqFosGWb6+YSlSSsKoQOrurzCjtxQIqEAR9SMmnqHGVWOQrXXhVVU3jHA5GdP2JmCvSBCskS/tXd9JH8546+8SG4YiHTTg/ek6skmMwhXFgVcZGZHxyeJWijPI/u72nNUMSHkbZTsKglWahBJgiQcEKiYJ9Eka8YGerS8hp2Av34Lgu6kZw4TIPDfhAwCvk4yNpc/RkWypjdZb1zqHr3iwhgC6RUu/9L5rUy8aSDVfxH1GjbjaKbPCQkIKBwIDAQABAoIBACT2BThenUn6aIZeevKza76qVGET22LEDt5Fmo1kLImZE7aMEuvgd/MzfmWNFcliwNrRdraDG4506ZfSBiv91YS71WlNnfiIE+fmfwHVvjRvvh/RsWbwBnABagg9XFbBfny2OFPj13z5tMHQjZVK99YRy0eylLuDtx/nsSG30Vao82z+CIYoQO6jRGpAhgkm5A662uTYDxfDJ4J5vTyJRURF+QCqOb8qlRDL/ZV+ybDzUr0n5Qul3eP5n9q9JQd9Qnb2xKyS8t6ngXUXleSWHcbxcD8tAMLtFLYNjOHdsa4IelqrK5jM6e+Yl3EMLPfa4pXLBnKCdTmNcTpTXsVnmUECgYEA71jUCm3HZdB2DcG5UiRjmO9vWQNUv06uFB5Q6VM+gTDPPK5u4kz1ZRcmrAboDoRKubZHc+mooOk1C5wGaWrxHy4oBBSr1VIe5JKI6+5aBB+PlKEVIElwdANbU+1kPKytA5MeDHOof1AgvtheYltsLU4vXLG+DAuj98G7VweUZF8CgYEAxmFx9Drc8LKXt+tov4Ov5/OaPSPcezKUN+KV5semIdNkYtjuO1yTi73yNxevvRZXYN/efcG5/TJyx4Fyvp2y4HC6LXOoj1clX60pHxqWi1Lz04GJEfByqSgYBR0kMZjodgOgOOOuqPQFTywke/2lFUPF8tAVPyJLPp4jXVX++1kCgYBFvJTzgO7jHGz5LyOm6lFWoxTHU7AimXMhC4A5q3Z/v8/x90T5jMDHNoqe/tgoOqVnHNQO0tq+H5TEEC7SEkW09wbTwY4bdnTn1kYsr+LsZqG4BYMZSCyKsNuwRW+6OfmjG/9aU2yZw6f20yYU9Fw9ixVDpcogyld4/apu/hdfcwKBgCMc/F6OTK0N72zObiv30xrrM1G/Fzd3LGT35jCDBhTWpd4ZJ5G6QSNq64R03NZLLgwnk+oOcC0w0MAfWYADybWQPmPtJNi6RBM7QxwOSLdAZ4f4VZqnRKRMRHQjRFTDC+JXofRv2GpvRsFMvuhzbNTmuhLQYfJaz5a1xuyuXAOBAoGBAKkbeNyGMnuHxgJ+sWCI5pM4w5/w1iovWu3qIEaCAZqt5PW1OvtcsKtDbzq+gdnWrsvX0bjx4LWcADi7wYGDMEo9WxBw2745hIUw3d/wDL98RnT/pCqpOhxtqzfIl+HdUbZEO23HVBm8atHgh/0eWCoYw99z3geQMY1iTi8gbpaA",
            "./cert/appCertPublicKey_2021000121674567.crt",
            "./cert/alipayRootCert.crt",
            "./cert/alipayCertPublicKey_RSA2.crt"
        );
        var wsServer = new WebSocketServer("ws://localhost:11451");
        var cinemaGen = new IdGenerator(ActionType.Cinema);
        var orderGen = new IdGenerator(ActionType.Order);
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

        cinemaProvider = new CinemaProvider(
            scheduleProviderF,
            cinemaGen,
            db
        );
        filmProvider = new FilmProvider(filmGen, db);
        orderProvider = new OrderProvider(
            userProviderF,
            filmProvider,
            new FilmOperation(db),
            cinemaProviderF,
            scheduleProviderF,
            orderGen, db
        );
        scheduleProvider = new ScheduleProvider(
            cinemaProviderF,
            filmProvider,
            scheduleGen,
            db
        );
        userProvider = new UserProvider(orderProviderF, db);
        vipProvider = new VipProvider(db);

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
        wsServer.AddWebSocketService<GetAllAvailableCinemaByFilmId>
        ("/get_all_available_cinema_by_film_id",
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
        wsServer.AddWebSocketService<IsOrderCanCheckIn>
        ("/is_order_can_check_in",
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
        wsServer.AddWebSocketService<GetUser>
        ("/get_user",
            handler => handler.Set(userProviderF()));
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