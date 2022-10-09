namespace SuperMovie.Server.Api.Statistics;

public struct GetReleasedFilmActorBorReq
{
}

public struct FilmActorBoxOfficeRsp
{
    public string FilmActor;
    public string FilmBoxOffice;
}

public struct GetReleasedFilmActorBorRsp
{
    public List<FilmActorBoxOfficeRsp> Collection;
}

//api : get_released_film_actor_bor
public class GetReleasedFilmActorBor
{
}