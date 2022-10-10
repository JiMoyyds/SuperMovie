namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmTypeBorReq
{
}

public struct FilmTypeBoxOfficeRsp
{
    public string FilmType;
    public long FilmBoxOffice;
}

public struct GetReleasedFilmTypeBorRsp
{
    public List<FilmTypeBoxOfficeRsp> Collection;
}

//api : get_released_film_type_bor
public class GetReleasedFilmTypeBor : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<GetReleasedFilmTypeBorReq>(e.Data);
        var rsp = new GetReleasedFilmTypeBorRsp
        {
            Collection = new List<FilmTypeBoxOfficeRsp>()
        };
    }
}