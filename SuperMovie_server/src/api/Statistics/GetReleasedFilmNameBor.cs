namespace SuperMovie.Server.Api.Statistics;

public struct GetReleasedFilmNameBorReq
{
}

public struct FilmNameBoxOfficeRsp
{
    public string FilmName;
    public string FilmBoxOffice;
}

public struct GetReleasedFilmNameBorRsp
{
    public List<FilmNameBoxOfficeRsp> Collection;
}

//api : get_released_film_name_bor
public class GetReleasedFilmNameBor
{
}