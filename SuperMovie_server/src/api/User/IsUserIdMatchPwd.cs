namespace SuperMovie_server.api.User;

public struct IsUserIdMatchPwdReq
{
    public long UserId;
    public long UserPwd;
}

public struct IsUserIdMatchPwdRsp
{
    public bool Ok;
}

//api : is_user_id_match_pwd
public class IsUserIdMatchPwd
{
}