namespace SuperMovieSDK.Container.Vip.Provider;

using  Entity;

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