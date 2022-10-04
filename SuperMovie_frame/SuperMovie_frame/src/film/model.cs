namespace SuperMovieSDK.Film.Model;

//由zty实现
public interface IFilmModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime OnlineTime { get; set; }
    public bool IsPreorder { get; set; }
    public List<string> Types { get; set; }
    public double Price { get; set; }
    public List<string> Actors { get; set; }
}