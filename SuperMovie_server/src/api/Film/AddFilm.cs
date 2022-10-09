namespace SuperMovie.Server.Api.Film;

public struct AddFilmReq
{
    public string FilmName;
    public string FilmCoverUrl;
    public string FilmPreviewVideoUrl;
    public double FilmPrice;
    public bool FilmIsPreorder;
}

public struct AddFilmRsp
{
    public bool Ok;
    public string FilmId;
}

//api : add_film
public class AddFilm
{
}