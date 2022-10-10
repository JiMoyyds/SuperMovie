namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmNameBorReq
{
}

public struct FilmNameBoxOfficeRsp
{
    public string FilmName;
    public long FilmBoxOffice;
}

public struct GetReleasedFilmNameBorRsp
{
    public List<FilmNameBoxOfficeRsp> Collection;
}

//api : get_released_film_name_bor
public class GetReleasedFilmNameBor : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<GetReleasedFilmNameBorReq>(e.Data);
        var rsp = new GetReleasedFilmNameBorRsp
        {
            Collection = new List<FilmNameBoxOfficeRsp>()
        };
       
        Send(JsonHelper.Stringify(rsp));
    }
}