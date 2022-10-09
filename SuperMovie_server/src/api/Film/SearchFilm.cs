namespace SuperMovie.Server.Api.Film;

public struct SearchFilmReq
{
    public List<string> FilmTypes;
    public List<DateTime> FilmOnlineTime;
    public List<DateTime> FilmScheduleStartTime;
    public string FilmNameKeyWord;
}

//api : search_film
public class SearchFilm
{
}