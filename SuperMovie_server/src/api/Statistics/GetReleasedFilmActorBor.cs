using SuperMovie.Container.Order.Provider;

namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmActorBorReq
{
}

public struct FilmActorBoxOfficeRsp
{
    public string FilmActor;
    public double FilmBoxOffice;
}

public struct GetReleasedFilmActorBorRsp
{
    public List<FilmActorBoxOfficeRsp> Collection;
}

//api : get_released_film_actor_bor
public class GetReleasedFilmActorBor : WebSocketBehavior
{
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_released_film_actor_bor req:\n{e.Data}");

        var req = JsonHelper.Parse<GetReleasedFilmActorBorReq>(e.Data);
        var bor = _orderProvider.GetReleasedFilmActorBor();

        var collection = new List<FilmActorBoxOfficeRsp>();

        foreach (var bo in bor)
        {
            var it = new FilmActorBoxOfficeRsp
            {
                FilmActor = bo.filmActor,
                FilmBoxOffice = bo.boxOffice
            };
            collection.Add(it);
        }

        var rsp = new GetReleasedFilmActorBorRsp
        {
            Collection = collection
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_released_film_actor_bor rsp:\n{json}");
        Send(json);
    }
}