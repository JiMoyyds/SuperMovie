namespace SuperMovie.Server.Api.Film;

using Container.Film.Provider;
using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct SearchFilmReq
{
    public List<string> FilmTypes;
    public List<DateTime> FilmOnlineTime;
    public List<DateTime> FilmScheduleStartTime;
    public string FilmNameKeyWord;
}

public struct SearchFilmRsp
{
    public List<FilmRsp> Collection;
}

//api : search_film
public class SearchFilm : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<SearchFilmReq>(e.Data);
        var rsp = new SearchFilmRsp
        {
            Collection = new List<FilmRsp>()
        };
    }
}