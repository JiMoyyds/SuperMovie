namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct DeleteFilmReq
{
    public long FilmId;
}

public struct DeleteFilmRsp
{
    public bool Ok;
}

//api : delete_film
public class DeleteFilm : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"delete_film req:\n{e.Data}");

        var req = JsonHelper.Parse<DeleteFilmReq>(e.Data);
        var film = _filmProvider.GetFilm(req.FilmId);

        DeleteFilmRsp rsp;

        if (film != null && film.Drop())
        {
            rsp = new DeleteFilmRsp
            {
                Ok = true
            };
        }
        else
        {
            rsp = new DeleteFilmRsp
            {
                Ok = false
            };
        }

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"delete_film rsp:\n{json}");
        Send(json);
    }
}