namespace SuperMovie.Container.Vip.Provider;

using  Entity;

//resolver : HZC
//asm file : VipContainerImpl
//impl :: SuperMovie.Container.Vip.Provider.VipProvider
//impl proj struct :
//src/container/provider.cs
public interface IVipProvider
{
    public IVipEntity? CreateVip
    (
        long level,
        double discount
    );

    public IVipEntity? GetVip(long level);
    public List<IVipEntity> GetAllVip();
}