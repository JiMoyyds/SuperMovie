namespace SuperMovieSDK;

public interface IVipModel
{
    public long Level { get; set; }
    public double Discount { get; set; }
}

public interface IVipOperation
{
    public bool CreateVip(IVipModel model);
    public bool DeleteVip(long userId);
    public List<IVipModel> GetAllVip();
    public bool UpdateVipDiscount(long vipId, long newValue);
}