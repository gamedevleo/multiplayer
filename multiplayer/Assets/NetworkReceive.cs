using System;
using KaymakNetwork;
using UnityEngine;

enum ServerPackets
{
    SWelcomeMsg =1,
    SInstantiatePlayer,
    SPlayerMove,
    SPlayerRotation
}

internal static class NetworkReceive
{
    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = new KaymakNetwork.Network.Client.DataArgs(Packet_WelcomeMsg);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = new KaymakNetwork.Network.Client.DataArgs(Packet_InstantiateNetworkPlayer);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerMove] = new KaymakNetwork.Network.Client.DataArgs(Packet_PlayerMove);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerRotation] = new KaymakNetwork.Network.Client.DataArgs(Packet_PlayerRotation);

    }
    private static void Packet_WelcomeMsg(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        string msg = buffer.ReadString();
        buffer.Dispose();
        NetworkManager.instance.myConnectionID = connectionID;
        //Debug.Log(msg);
        NetworkSend.SendPing();
    }

    private static void Packet_InstantiateNetworkPlayer(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();

    if (connectionID == NetworkManager.instance.myConnectionID)
        NetworkManager.instance.InstantiateNetworkPlayer(connectionID, true);
    else
        NetworkManager.instance.InstantiateNetworkPlayer(connectionID, false);
    }

    private static void Packet_PlayerMove(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        float x = buffer.ReadSingle();
        float y = buffer.ReadSingle();
        float z = buffer.ReadSingle();
        buffer.Dispose();

        if (!GameManager.instance.playerList.ContainsKey(connectionID)) return;

        GameManager.instance.playerList[connectionID].transform.position = new Vector3(x, y, z);
    }
    private static void Packet_PlayerRotation(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        float rotation = GameManager.instance.WrapEulerAngles(buffer.ReadSingle());

        if (connectionID == NetworkManager.instance.myConnectionID) return;
        if (!GameManager.instance.playerList.ContainsKey(connectionID)) return;
        GameManager.instance.playerList[connectionID].transform.rotation = new Quaternion(0, rotation, 0, 0);
        buffer.Dispose();
    }


}
