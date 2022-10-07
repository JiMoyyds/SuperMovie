namespace SuperMovie.Db.Cinema.Operation;

using Model;

//resolver : WZJ
//asm file : CinemaImpl
//impl :: SuperMovie.Db.Cinema.Operation.CinemaOperation
//impl proj struct :
//src/db/operation.cs
public interface ICinemaOperation
{
    public bool CreateCinema(ICinemaModel model);
    public bool DeleteCinema(long cinemaId);
    public ICinemaModel? GetCinema(long cinemaId);
    public List<ICinemaModel> GetAllCinema();
    public bool UpdateCinemaName(long cinemaId, string newValue);
}