namespace SuperMovieSDK;

public interface ICinemaModel
{
    public long CinemaId { get; set; }
    public long CinemaName { get; set; }
}

public interface ICinemaOperation
{
    public bool CreateCinema(ICinemaModel model);
    public List<ICinemaModel> GetAllCinema();
}