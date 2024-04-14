namespace Script.Network
{
    /// <summary>
    /// 
    /// </summary>
    public enum RequestType
    {
        Request,
        BroadCast
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
    public enum OperationType
    {
        Error,
        /// <summary>
        /// 
        /// </summary>
        Init,
        /// <summary>
        /// 申请一个NetworkObject的唯一id
        /// </summary>
        CreateObject,
        Card,
        Skill,
        /// <summary>
        /// 尝试连接,无房间就创建一个
        /// </summary>
        TryConnectRoom,
        EndTurn,
        Debug,
        Effect
    }
}