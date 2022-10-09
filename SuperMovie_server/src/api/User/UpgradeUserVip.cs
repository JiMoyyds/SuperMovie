namespace SuperMovie_server.api.User;

public struct UpgradeUserVipReq
{
    public long UserId;
    public long NewVipLevel;
}

public struct UpgradeUserVipRsp
{
    public bool Ok;
}

//api : upgrade_user_vip
public class UpgradeUserVip
{
}