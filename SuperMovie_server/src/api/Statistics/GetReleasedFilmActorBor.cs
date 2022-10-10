namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;
using Util;

public struct GetReleasedFilmActorBorReq
{
}

public struct FilmActorBoxOfficeRsp
{
    public string FilmActor;
    public long FilmBoxOffice;
}

public struct GetReleasedFilmActorBorRsp
{
    public List<FilmActorBoxOfficeRsp> Collection;
}

//api : get_released_film_actor_bor
public class GetReleasedFilmActorBor : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        var req = JsonHelper.Parse<GetReleasedFilmActorBorReq>(e.Data);
        var rsp = new GetReleasedFilmActorBorRsp
        {
            Collection = new List<FilmActorBoxOfficeRsp>()
        };
       
        Send(JsonHelper.Stringify(rsp));
    }
}