namespace SuperMovie.Server.Api.Film;

public struct GetAllFilmReq
{
}

public struct FilmRsp
{
    public string FilmId;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct GetAllFilmRsp
{
    public List<FilmRsp> Collection;
}

//api : get_all_film
public class GetAllFilm
{
}