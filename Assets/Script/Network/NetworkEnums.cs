namespace Script.Network
{
    //枚举将0空出来,因为默认值就是0,不填代表错误
    
    /// <summary>
    /// 一些网络相关的状态码
    /// </summary>
    public enum StatusCode
    {
        NetworkError=1,
        Ok=2,
        RoomIdError=3,
    }
    /// <summary>
    /// 自己是哪个Player
    /// </summary>
    public enum PlayerEnum
    {
        Player1=1,
        Player2=2,
        NotReady=3
    }

    public enum OperationCode
    {
        
    }
}