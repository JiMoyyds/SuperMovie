namespace SuperMovieSDK.Container.Film.Entity;

public interface IFilmEntity
{
    public bool Drop();

    public string Name { get; set; }
    public DateTime OnlineTime { get; set; }
    public bool IsPreorder { get; set; }

    public List<string> Types { get; }
    public bool AddType(string name);
    public bool RemoveType(string name);
    public bool ClearType(string name);

    public double Price { get; set; }

    public List<string> Actors { get; }
    public bool AddActor(string name);
    public bool RemoveActor(string name);
    public bool ClearActor(string name);
}