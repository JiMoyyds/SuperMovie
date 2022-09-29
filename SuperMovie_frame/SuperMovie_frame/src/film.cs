namespace SuperMovieSDK;

public interface IFilm
{
    public long FilmId { get; set; }
    public long FilmName { get; set; }
    public DateTime OnlineTime { get; set; }
    public bool IsPreorder { get; set; }
    public string FilmType { get; set; }
}