namespace SuperMovieSDK.Container.Film.Provider;

using Entity;

public interface IFilmProvider
{
    public IFilmEntity? CreateFilm
    (
        string name,
        DateTime onlineTime,
        bool isPreorder,
        double price
    );

    public IFilmEntity? GetFilm(long id);
    public List<IFilmEntity> GetAllFilm();

    public List<IFilmEntity> FilterFilmByIsPreorder(bool isPreorder);
    public List<IFilmEntity> FilterFilmByTypes(List<string> types);
    public List<IFilmEntity> FilterFilmByOnlineTime(DateTime start, DateTime end);
    public List<IFilmEntity> FilterFilmByName(string name);
    public List<IFilmEntity> MatchFilmByName(string name);
    public List<IFilmEntity> FilterFilmByActors(List<string> actors);
}