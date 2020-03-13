using System;
using KaymakNetwork;


enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CPlayerRotation
}
internal static class NetworkSend
{
    public static void SendPing()

    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPing);
        buffer.WriteString("Hello I'm the client thank you for letting me connecting to the server!");
        NetworkConfig.socket.SendData(buffer.Data,buffer.Head);

        buffer.Dispose(); 
    }

    public static void SendKeyInput(InputManager.Keys pressedKey)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CKeyInput);
        buffer.WriteByte((byte)pressedKey);
        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    public static void SendPlayerRotation(float rotation)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ClientPackets.CPlayerRotation);
        buffer.WriteSingle(rotation);
        NetworkConfig.socket.SendData(buffer.Data, buffer.Head);

        buffer.Dispose();
    }

}
