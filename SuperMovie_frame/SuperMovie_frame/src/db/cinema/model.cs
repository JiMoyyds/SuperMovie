namespace SuperMovie.Db.Cinema.Model;

//resolver : WZJ
//asm file : CinemaImpl
//impl :: SuperMovie.Db.Cinema.Model.CinemaModel
//impl proj struct :
//src/db/model.cs
public interface ICinemaModel
{
    public long Id { get; set; }
    public string Name { get; set; }
}