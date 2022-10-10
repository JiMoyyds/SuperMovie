using SuperMovie.Container.Order.Provider;

namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmTypeBorReq
{
}

public struct FilmTypeBoxOfficeRsp
{
    public string FilmType;
    public double FilmBoxOffice;
}

public struct GetReleasedFilmTypeBorRsp
{
    public List<FilmTypeBoxOfficeRsp> Collection;
}

//api : get_released_film_type_bor
public class GetReleasedFilmTypeBor : WebSocketBehavior
{
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_released_film_type_bor req:\n{e.Data}");

        var req = JsonHelper.Parse<GetReleasedFilmTypeBorReq>(e.Data);
        var bor = _orderProvider.GetReleasedFilmTypeBor();

        var collection = new List<FilmTypeBoxOfficeRsp>();

        foreach (var bo in bor)
        {
            var it = new FilmTypeBoxOfficeRsp
            {
                FilmType = bo.filmType,
                FilmBoxOffice = bo.boxOffice
            };
            collection.Add(it);
        }

        var rsp = new GetReleasedFilmTypeBorRsp
        {
            Collection = collection
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_released_film_type_bor rsp:\n{json}");
        Send(json);
    }
}