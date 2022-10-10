namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllFilmOnlineTimeReq
{
}

public struct GetAllFilmOnlineTimeRsp
{
    public List<DateTime> Collection;
}

//api : get_all_film_online_time
public class GetAllFilmOnlineTime : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_film req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllFilmOnlineTimeReq>(e.Data);
        var timeList = _filmProvider.GetAllFilmOnlineTime();

        var rsp = new GetAllFilmOnlineTimeRsp
        {
            Collection = timeList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_film rsp:\n{json}");
        Send(json);
    }
}