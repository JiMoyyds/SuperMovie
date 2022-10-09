namespace SuperMovie.Server.Api.Film;

public struct SearchFilmReq
{
    public List<string> FilmTypes;
    public List<DateTime> FilmOnlineTime;
    public List<DateTime> FilmScheduleStartTime;
    public string FilmNameKeyWord;
}

public struct SearchFilmRsp
{
    public List<FilmRsp> Collection;
}

//api : search_film
public class SearchFilm
{
}