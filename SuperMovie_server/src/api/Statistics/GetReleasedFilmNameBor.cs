namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}