namespace SuperMovie.Server.Api.Statistics;

using Container.Order.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmNameBorReq
{
}

public struct FilmNameBoxOfficeRsp
{
    public string FilmName;
    public double FilmBoxOffice;
}

public struct GetReleasedFilmNameBorRsp
{
    public List<FilmNameBoxOfficeRsp> Collection;
}

//api : get_released_film_name_bor
public class GetReleasedFilmNameBor : WebSocketBehavior
{
    private IOrderProvider _orderProvider;

    public void Set(IOrderProvider orderProvider)
    {
        _orderProvider = orderProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_released_film_name_bor req:\n{e.Data}");

        var req = JsonHelper.Parse<GetReleasedFilmNameBorReq>(e.Data);
        var bor = _orderProvider.GetReleasedFilmNameBor();

        var collection = new List<FilmNameBoxOfficeRsp>();

        foreach (var bo in bor)
        {
            var it = new FilmNameBoxOfficeRsp
            {
                FilmName = bo.filmName,
                FilmBoxOffice = bo.boxOffice
            };
            collection.Add(it);
        }

        var rsp = new GetReleasedFilmNameBorRsp
        {
            Collection = collection
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_released_film_name_bor rsp:\n{json}");
        Send(json);
    }
}