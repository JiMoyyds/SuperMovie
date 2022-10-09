namespace SuperMovie.Server.Api.Film;

public class UpdateFilmReq
{
    public long FilmId;
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

//api : update_film
public class UpdateFilm
{
}