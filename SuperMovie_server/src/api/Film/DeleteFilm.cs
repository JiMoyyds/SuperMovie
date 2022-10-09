namespace SuperMovie.Server.Api.Film;

public struct DeleteFilmReq
{
    public long FilmId;
}

public struct DeleteFilmRsp
{
    public bool Ok;
}

//api : delete_film
public class DeleteFilm
{
}