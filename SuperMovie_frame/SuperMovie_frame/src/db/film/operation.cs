namespace SuperMovieSDK.Db.Film.Operation;

using Model;

//由zty实现
public interface IFilmOperation
{
    public bool CreateFilm(IFilmModel model);
    public bool DeleteFilm(long filmId);
    public IFilmModel? GetFilm(long filmId);
    public List<IFilmModel> GetAllFilm();
    public List<IFilmModel> FilterFilmByIsPreorder(bool isPreorder);
    public List<IFilmModel> FilterFilmByTypes(List<string> types);
    public List<IFilmModel> FilterFilmByOnlineTime(DateTime start, DateTime end);
    public List<IFilmModel> FilterFilmByName(string name);
    public List<IFilmModel> MatchFilmByName(string name);
    public List<IFilmModel> FilterFilmByActors(List<string> actors);
    public List<string> GetAllActors();
    public bool UpdateFilmName(long filmId, string newValue);
    public bool UpdateFilmOnlineTime(long filmId, DateTime newValue);
    public bool UpdateFilmIsPreorder(long filmId, bool newValue);
    public bool UpdateFilmTypes(long filmId, List<string> newValue);
    public bool UpdateFilmPrice(long filmId, double newValue);
    public bool UpdateFilmActors(long filmId, List<string> newValue);
}