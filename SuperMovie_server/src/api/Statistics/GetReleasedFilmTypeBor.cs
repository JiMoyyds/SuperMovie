namespace SuperMovie.Server.Api.Statistics;

using WebSocketSharp;
using WebSocketSharp.Server;

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
}