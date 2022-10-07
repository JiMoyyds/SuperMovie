namespace SuperMovie.Db.Schedule.Model;

//resolver : WZJ
//asm file : ScheduleImpl
//impl :: SuperMovie.Db.Schedule.Model.ScheduleModel
//impl proj struct :
//src/db/model.cs
public interface IScheduleModel
{
    public long Id { get; set; }
    public long CinemaId { get; set; }
    public long FilmId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}