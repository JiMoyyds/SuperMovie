namespace SuperMovieSDK.Container.Vip.Entity;

public interface IVipEntity
{
    public bool Drop();

    public long Level { get; set; }
    public long Discount { get; set; }
}