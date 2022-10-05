namespace SuperMovieSDK.Container.Cinema.Provider;

using Entity;

public interface ICinemaProvider
{
    public ICinemaEntity? CreateCinema(string name);
    public ICinemaEntity? GetCinema(long id);
    public List<ICinemaEntity> GetAllCinema();
}