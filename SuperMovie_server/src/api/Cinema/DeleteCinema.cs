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
    private readonly ICinemaProvider _cinemaProvider;

    public DeleteCinema(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
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

        Send(JsonHelper.Stringify(rsp));
    }
}