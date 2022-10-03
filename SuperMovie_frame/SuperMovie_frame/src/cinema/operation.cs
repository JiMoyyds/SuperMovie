namespace SuperMovieSDK.Cinema.Operation;

using Model;

//由wzj实现
public interface ICinemaOperation
{
    public bool CreateCinema(ICinemaModel model);
    public List<ICinemaModel> GetAllCinema();
    public bool UpdateCinemaName(long cinemaId, string newValue);
}