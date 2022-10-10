using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Provider;

namespace SuperMovie.Server.Api.Order;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct CreateOrderReq
{
    public long OrderFilmId;
    public long OrderUserId;
    public long OrderCinemaId;
    public long OrderScheduleId;
    public string OrderSeat;
    public double OrderPayAmount;
}

public struct CreateOrderRsp
{
    public bool Ok;
    public long OrderId;
}

//api : create_order
public class CreateOrder : WebSocketBehavior
{
    private IUserProvider _userProvider;
    private IFilmProvider _filmProvider;
    private ICinemaProvider _cinemaProvider;
    private IScheduleProvider _scheduleProvider;
    private IOrderProvider _orderProvider;

    public void Set(
        IUserProvider userProvider,
        IFilmProvider filmProvider,
        ICinemaProvider cinemaProvider,
        IScheduleProvider scheduleProvider,
        IOrderProvider orderProvider
    )
    {
        _userProvider = userProvider;
        _filmProvider = filmProvider;
        _cinemaProvider = cinemaProvider;
        _scheduleProvider = scheduleProvider;
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"create_order req:\n{e.Data}");

        var req = JsonHelper.Parse<CreateOrderReq>(e.Data);
        var user = _userProvider.GetUser(req.OrderUserId);
        var film = _filmProvider.GetFilm(req.OrderFilmId);
        var cinema = _cinemaProvider.GetCinema(req.OrderCinemaId);
        var schedule = _scheduleProvider.GetSchedule(req.OrderScheduleId);

        CreateOrderRsp rsp;

        if (user != null && film != null && cinema != null && schedule != null)
        {
            var order = _orderProvider.CreateOrder
            (
                user,
                req.OrderPayAmount,
                film,
                cinema,
                schedule,
                req.OrderSeat
            );

            if (order != null)
            {
                rsp = new CreateOrderRsp
                {
                    Ok = true,
                    OrderId = order.Id
                };
            }
            else
            {
                rsp = new CreateOrderRsp
                {
                    Ok = false,
                    OrderId = 0
                };
            }
        }
        else
        {
            rsp = new CreateOrderRsp
            {
                Ok = false,
                OrderId = 0
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"create_order rsp:\n{json}");
        Send(json);
    }
}