namespace SuperMovieSDK.Db.Vip.Operation;

using Model;

//由hzc实现
public interface IVipOperation
{
    public bool CreateVip(IVipModel model);
    public bool DeleteVip(long vipLevel);
    public IVipModel? GetVip(long vipLevel);
    public List<IVipModel> GetAllVip();
    public bool UpdateVipDiscount(long vipLevel, double newValue);
}