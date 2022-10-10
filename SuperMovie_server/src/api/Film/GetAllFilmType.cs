namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetAllFilmTypeReq
{
}

public struct GetAllFilmTypeRsp
{
    public List<string> Collection;
}

//api : get_all_film_type
public class GetAllFilmType : WebSocketBehavior
{
    private IFilmProvider _filmProvider;

    public void Set(IFilmProvider filmProvider)
    {
        _filmProvider = filmProvider;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"get_all_film_type req:\n{e.Data}");

        var req = JsonHelper.Parse<GetAllFilmTypeReq>(e.Data);
        var typeList = _filmProvider.GetAllFilmType();

        var rsp = new GetAllFilmTypeRsp
        {
            Collection = typeList
        };

        var json = JsonHelper.Stringify(rsp);
        Console.WriteLine($"get_all_film_type rsp:\n{json}");
        Send(json);
    }
}