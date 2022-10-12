namespace SuperMovie.Server.Api.Cinema;

using Container.Cinema.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetCinemaReq
{
    public long CinemaId;
}

public struct GetCinemaRsp
{
    public bool Ok;
    public long CinemaId;
    public string CinemaName;
}

//api : get_cinema
public class GetCinema : WebSocketBehavior
{
    private ICinemaProvider _cinemaProvider;

    public void Set(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_cinema req:\n{e.Data}");

        var req = JsonHelper.Parse<GetCinemaReq>(e.Data);
        var cinema = _cinemaProvider.GetCinema(req.CinemaId);

        GetCinemaRsp rsp;

        if (cinema != null)
        {
            rsp = new GetCinemaRsp
            {
                Ok = true,
                CinemaId = cinema.Id,
                CinemaName = cinema.Name
            };
        }
        else
        {
            rsp = new GetCinemaRsp
            {
                Ok = false,
                CinemaId = 0,
                CinemaName = ""
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_cinema rsp:\n{json}");
        Send(json);
    }
}