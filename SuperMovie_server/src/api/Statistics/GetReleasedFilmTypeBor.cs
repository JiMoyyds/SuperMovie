namespace SuperMovie.Server.Api.Statistics;

public struct GetReleasedFilmTypeBorReq
{
}

public struct FilmTypeBoxOfficeRsp
{
    public string FilmType;
    public string FilmBoxOffice;
}

public struct GetReleasedFilmTypeBorRsp
{
    public List<FilmTypeBoxOfficeRsp> Collection;
}

//api : get_released_film_type_bor
public class GetReleasedFilmTypeBor
{
}