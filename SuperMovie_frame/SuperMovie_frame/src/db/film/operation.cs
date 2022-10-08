namespace SuperMovie.Db.Film.Operation;

using Model;

//resolver : ZTY
//asm file : FilmDbImpl
//impl :: SuperMovie.Db.Film.Operation.FilmOperation
//impl proj struct :
//src/db/operation.cs
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
    public bool UpdateFilmCoverUrl(long filmId, string newValue);
    public bool UpdateFilmPreviewVideoUrl(long filmId, string newValue);
}