namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}