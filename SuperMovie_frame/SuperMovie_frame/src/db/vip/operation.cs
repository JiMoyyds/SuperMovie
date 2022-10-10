namespace SuperMovie.Db.Vip.Operation;

using Model;

//resolver : HZC
//asm file : VipDbImpl
//impl :: SuperMovie.Db.Vip.Operation.VipOperation
//impl proj struct :
//src/db/operation.cs
public interface IVipOperation
{
    public bool CreateVip(IVipModel model);
    public bool DeleteVip(long vipLevel);
    public IVipModel? GetVip(long vipLevel);
    public List<IVipModel> GetAllVip();
    public bool UpdateVipDiscount(long vipLevel, double newValue);
    public bool UpdateVipMonthPrice(long vipLevel, double newValue);
}