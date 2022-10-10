namespace SuperMovie.Server.Api.Cinema;

using Container.Cinema.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllCinemaReq
{
}

public struct CinemaRsp
{
    public long CinemaId;
    public string CinemaName;
}

public struct GetAllCinemaRsp
{
    public List<CinemaRsp> Collection;
}

//api : get_all_cinema
public class GetAllCinema : WebSocketBehavior
{
    private ICinemaProvider _cinemaProvider;

    public void Set(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_cinema req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllCinemaReq>(e.Data);
        var cinemas = _cinemaProvider.GetAllCinema();

        var cinemaRspList = new List<CinemaRsp>();

        foreach (var cinema in cinemas)
        {
            var cinemaRsp = new CinemaRsp
            {
                CinemaId = cinema.Id,
                CinemaName = cinema.Name
            };
            cinemaRspList.Add(cinemaRsp);
        }

        GetAllCinemaRsp rsp = new GetAllCinemaRsp
        {
            Collection = cinemaRspList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_cinema rsp:\n{json}");
        Send(json);
    }
}