namespace SuperMovie.Container.Film.Entity;

//resolver : ZTY
//asm file : FilmContainerImpl
//impl :: SuperMovie.Container.Film.Entity.FilmEntity
//impl proj struct :
//src/container/entity.cs
public interface IFilmEntity
{
    /// <summary>
    /// 释放实体
    /// </summary>
    /// <returns></returns>
    public bool Drop();

    /// <summary>
    /// 实体是否合法
    /// </summary>
    /// <returns></returns>
    public bool IsValid();

    public long Id { get; }

    public string? Name { get; set; }
    public DateTime? OnlineTime { get; set; }
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

    public string CoverUrl { get; set; }
    public string PreviewVideoUrl { get; set; }
}