namespace SuperMovie.Server.Api.Film;

public struct UpdateFilmReq
{
    public long FilmId;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct UpdateFilmRsp
{
    public bool Ok;
}

//api : update_film
public class UpdateFilm
{
}