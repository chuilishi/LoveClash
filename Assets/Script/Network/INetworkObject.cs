namespace Script.Network
{
    public interface INetworkObject
    {
        public int networkId { get; set; }
        public ObjectEnum ObjectEnum { get; set; }
    }
}