using SuperMovie.Container.User.Provider;

namespace SuperMovie.Server.Api.Order;

using Container.User.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllOrderWithUserIdReq
{
    public long OrderUserId;
}

public struct GetAllOrderWithUserIdRsp
{
    public List<OrderRsp> Collection;
}

//api : get_all_order_with_user_id
public class GetAllOrderWithUserId : WebSocketBehavior
{
    private IUserProvider _userProvider;

    public void Set(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_order_with_user_id req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllOrderWithUserIdReq>(e.Data);
        var user = _userProvider.GetUser(req.OrderUserId);

        var orderRspList = new List<OrderRsp>();

        if (user != null)
        {
            foreach (var order in user.Orders)
            {
                var orderRsp = new OrderRsp
                {
                    OrderId = order.Id,
                    OrderUserId = order.User.Id,
                    OrderFilmId = order.Film.Id,
                    OrderCinemaId = order.Cinema.Id,
                    OrderScheduleId = order.Schedule.Id,
                    OrderSeat = order.Seat,
                    OrderPayAmount = order.PayAmount
                };
                orderRspList.Add(orderRsp);
            }
        }

        var rsp = new GetAllOrderRsp
        {
            Collection = orderRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_order_with_user_id rsp:\n{json}");
        Send(json);
    }
}