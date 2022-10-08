namespace SuperMovie.Container.Cinema.Provider;

using Entity;

//resolver : WZJ
//asm file : CinemaContainerImpl
//impl :: SuperMovie.Container.Cinema.Provider.CinemaProvider
//impl proj struct :
//src/container/provider.cs
public interface ICinemaProvider
{
    public ICinemaEntity? CreateCinema(string name);
    public ICinemaEntity? GetCinema(long id);
    public List<ICinemaEntity> GetAllCinema();
}