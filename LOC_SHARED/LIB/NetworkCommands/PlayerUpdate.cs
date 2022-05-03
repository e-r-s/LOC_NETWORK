using System;
 
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;


using UnityEngine; 
namespace LOC_SHARED.NetworkCommands
{

    public class PlayerUpdateResult
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public bool IsWalking { get; set; }
        public bool IsRunning { get; set; }
        public bool IsAttacking { get; set; }
        public bool IsJumping { get; set; }
        public int UserId { get; set; }
    }


    public class PlayerUpdate : NetworkCommand<PlayerUpdateResult>
    {

        public PlayerUpdate() 
        {
            this._BufferSize = 44;
            this._Cmd = NetworkCommandType.PlayerActionCommands.PlayerData_Position;
            this._CmdGroup = NetworkCommandType.PlayerActionCommands.CommandGroup;
            base.InitPackets();
        }
   
        public void SendData(Vector3 position, Quaternion rotation, bool isWalking, bool isRunning, bool IsAttacking)
        {
            SendDataPacket.WriteBytes(position);
            SendDataPacket.WriteBytes(rotation);

            SendDataPacket.WriteBytes(isWalking);
            SendDataPacket.WriteBytes(isRunning);
            SendDataPacket.WriteBytes(IsAttacking);
            base.SendDataPacketToServer();
             
        }

        public PlayerUpdateResult ReceiveData(byte[] data, int startIndex, int length)
        {
            if (base.LoadBufferWithIncomingData(data,   startIndex,   length))
            {
                PlayerUpdateResult result = new PlayerUpdateResult();
                result.Position = this.ReceiveDataPacket.ReadVector3();
                result.Rotation = this.ReceiveDataPacket.ReadQuaternion();
                result.UserId = this.ReceiveDataPacket.GetPacketOwner();

                result.IsWalking = this.ReceiveDataPacket.ReadBoolean();
                result.IsRunning = this.ReceiveDataPacket.ReadBoolean();
                result.IsAttacking = this.ReceiveDataPacket.ReadBoolean();

                CallOnDataReceived(result);

                return result;

            }
           
            return null;
            
        }


    }
}
