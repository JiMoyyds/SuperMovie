using SuperMovie.Container.Cinema.Provider;
using SuperMovie.Container.Film.Provider;
using SuperMovie.Container.Order.Provider;
using SuperMovie.Container.Schedule.Provider;
using SuperMovie.Container.User.Provider;
using Aop.Api;
using AlipayF2F;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Domain;

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
    public long AlipayOrderId;
}

public struct CreateOrderRsp
{
    public bool Ok;
    public long OrderId;
    public string AlipayQrCodePath;
}

//api : create_order
public class CreateOrder : WebSocketBehavior
{
    private IUserProvider _userProvider;
    private IFilmProvider _filmProvider;
    private ICinemaProvider _cinemaProvider;
    private IScheduleProvider _scheduleProvider;
    private IOrderProvider _orderProvider;
    private F2FClient _f2fClient;

    public void Set(
        IUserProvider userProvider,
        IFilmProvider filmProvider,
        ICinemaProvider cinemaProvider,
        IScheduleProvider scheduleProvider,
        IOrderProvider orderProvider,
        F2FClient f2fCLient
    )
    {
        _userProvider = userProvider;
        _filmProvider = filmProvider;
        _cinemaProvider = cinemaProvider;
        _scheduleProvider = scheduleProvider;
        _orderProvider = orderProvider;
        _f2fClient = f2fCLient;
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
                var orderMsg =
                    $@"超级电影购票: {film.Name}
                       订单号: {order.Id}
                       影厅: {cinema.Name}
                       座位: {order.Seat}
                       场次: {order.Schedule.StartTime} to {order.Schedule.EndTime}";

                var f2fReq = F2FRequest.PreCreateTrade(
                    order.Id.ToString(),
                    orderMsg,
                    order.PayAmount
                );
                var f2fRsp = _f2fClient.ExecuteRequest(f2fReq);

                rsp = new CreateOrderRsp
                {
                    Ok = true,
                    OrderId = order.Id,
                    AlipayOrderId = ((AopResponse)f2fRsp)
                };
            }
            else
            {
                rsp = new CreateOrderRsp
                {
                    Ok = false,
                    OrderId = 0,
                    AlipayQrCodePath = ""
                };
            }
        }
        else
        {
            rsp = new CreateOrderRsp
            {
                Ok = false,
                OrderId = 0,
                AlipayQrCodePath = ""
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"create_order rsp:\n{json}");
        Send(json);
    }
}