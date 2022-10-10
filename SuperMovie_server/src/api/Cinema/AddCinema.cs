namespace SuperMovie.Server.Api.Cinema;

using Container.Cinema.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct AddCinemaReq
{
    public string CinemaName;
}

public struct AddCinemaRsp
{
    public bool Ok;
    public long CinemaId;
}

//api : add_cinema
public class AddCinema : WebSocketBehavior
{
    private readonly ICinemaProvider _cinemaProvider;

    public AddCinema(ICinemaProvider cinemaProvider)
    {
        _cinemaProvider = cinemaProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<AddCinemaReq>(e.Data);
        var cinema = _cinemaProvider.CreateCinema(req.CinemaName);

        AddCinemaRsp rsp;

        if (cinema != null)
        {
            rsp = new AddCinemaRsp
            {
                Ok = true,
                CinemaId = cinema.Id
            };
        }
        else
        {
            rsp = new AddCinemaRsp
            {
                Ok = false,
                CinemaId = 0
            };
        }

        Send(JsonHelper.Stringify(rsp));
    }
}