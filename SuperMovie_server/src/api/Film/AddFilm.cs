namespace SuperMovie.Server.Api.Film;

public struct AddFilmReq
{
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public double FilmIsPreorder;
}

//api : add_film
public class AddFilm
{
}