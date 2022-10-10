namespace SuperMovie.Server.Api.Cinema;

using Container.Cinema.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct DeleteCinemaReq
{
    public long CinemaId;
}

public struct DeleteCinemaRsp
{
    public bool Ok;
}

//api : deleta_cinema
public class DeleteCinema : WebSocketBehavior
{
    private ICinemaProvider _cinemaProvider;

    public void Set(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"delete_cinema req:\n{e.Data}");
        
        var req = JsonHelper.Parse<DeleteCinemaReq>(e.Data);
        var cinema = _cinemaProvider.GetCinema(req.CinemaId);

        DeleteCinemaRsp rsp;

        if (cinema != null && cinema.Drop())
        {
            rsp = new DeleteCinemaRsp
            {
                Ok = true,
            };
        }
        else
        {
            rsp = new DeleteCinemaRsp
            {
                Ok = false,
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"delete_cinema rsp:\n{json}");
        Send(json);
    }
}