namespace SuperMovieSDK;

public interface IFilmModel
{
    public long Id { get; set; }
    public long Name { get; set; }
    public DateTime OnlineTime { get; set; }
    public bool IsPreorder { get; set; }
    public List<string> Types { get; set; }
    public double Price { get; set; }
    public List<string> Actors { get; set; }
}

public interface IFilmOperation
{
    public bool CreateFilm(IFilmModel model);
    public bool DeleteFilm(long filmId);
    public List<IFilmModel> GetAllFilm();
    public List<IFilmModel> FilterFilmByIsPreorder(bool isPreorder);
    public List<IFilmModel> FilterFilmByTypes(List<string> types);
    public List<IFilmModel> FilterFilmByOnlineTime(DateTime start, DateTime end);
    public List<IFilmModel> FilterFilmByName(string name);
    public List<IFilmModel> FilterFilmByActors(List<string> actors);
    public bool UpdateFilmName(long filmId, string newValue);
    public bool UpdateFilmOnlineTime(long filmId, DateTime newValue);
    public bool UpdateFilmIsPreorder(long filmId, bool newValue);
    public bool UpdateFilmTypes(long filmId, List<string> newValue);
    public bool UpdateFilmPrice(long filmId, double newValue);
    public bool UpdateFilmActors(long filmId, List<string> newValue);
}