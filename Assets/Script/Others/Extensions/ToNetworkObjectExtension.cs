using Script.core;
using Script.Network;

public static class ToNetworkObjectExtension
{
    public static NetworkObject ToNetworkObject(this int networkId)
    {
        return NetworkManager.GetObjectById(networkId);
    }
}