using System;
 
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;
 

using UnityEngine; 

namespace LOC_SHARED.NetworkCommands
{

    public class PlayerLocationResult
    {

        public int ChunkId { get; set; }
        public int RegionId { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public int UserId { get; set; }

        public byte[] RawData { get; set; }

        //public byte[] ChunkIdBytes { get; set; }
        //public byte[] RegionIdBytes { get; set; }
        //public byte[] PositionBytes { get; set; }
        //public byte[] RotationBytes { get; set; } 

    }


    public class PlayerLocationChanged: NetworkCommand<PlayerLocationResult>
    {

        public PlayerLocationChanged() 
        {
            this._BufferSize = 36;
            this._Cmd = NetworkCommandType.PlayerActionCommands.PlayerData_Position;
            this._CmdGroup = NetworkCommandType.PlayerActionCommands.CommandGroup;
           
           // ClientNetworkManager.OnLoggedIn += ClientNetworkManager_OnLoggedIn;
            PacketManager.OnInit += PacketManager_OnInit;
        }

        private void PacketManager_OnInit()
        {
            base.InitPackets();
        }

        //private void ClientNetworkManager_OnLoggedIn(PlayerLoginResult loginData)
        //{
             //base.InitPackets();
        //}


        /// <summary>
        /// Used to send data from client to server
        /// </summary>
        /// <param name="chunkId"></param>
        /// <param name="regionId"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SendData(int chunkId, int regionId, Vector3 position, Quaternion rotation)
        {
            SendDataPacket.WriteBytes(chunkId);
            SendDataPacket.WriteBytes(regionId);
            SendDataPacket.WriteBytes(position);
            SendDataPacket.WriteBytes(rotation);
            base.SendDataPacketToServer();

        }

        public class MultipleDataResult
        {
            public RawCommandData[] ChildCommandData { get; set; }
            public short TotalDataSize { get; set; }
        }
         
        /// <summary>
        /// Converts location result to raw command data.
        /// </summary>
        /// <param name="locationResult">Location result to convert</param>
        /// <returns>Command data used to send multiple data</returns>
        public RawCommandData GetRawCommandData(PlayerLocationResult locationResult)
        {
            RawCommandData data = new RawCommandData();
            data.UserId = locationResult.UserId;
            data.CmdGroup = this.CmdGroup;
            data.Cmd = this.Cmd;
            data.Command = this;
            data.Data = new byte[this.BufferSize];
            data.CreatedTime = DateTime.Now.Ticks;

            //Packet.WriteBytes(data.Data, 0, locationResult.ChunkId);
            //Packet.WriteBytes(data.Data, 4, locationResult.RegionId);
            //Packet.WriteBytes(data.Data, 8, locationResult.Position);
            //Packet.WriteBytes(data.Data, 20, locationResult.Rotation);

            if (data.Data == null || locationResult.RawData==null)
            {
                Util.Logger.Log("d");
            }
            Packet.WriteBytes(data.Data, 0, locationResult.RawData);
             

            return data;  
        }

        /// <summary>
        /// Gets raw command data for this command with given parameters
        /// </summary>
        /// <param name="userId">Dta owner</param>
        /// <param name="chunkId">player located chunk. If no -1</param>
        /// <param name="regionId">player located region. if no -1</param>
        /// <param name="position">player located postion.</param>
        /// <param name="rotation">player rotation</param>
        /// <returns></returns>
        public RawCommandData GetRawCommandData(int userId, int chunkId, int regionId, Vector3 position, Quaternion rotation)
        {
            RawCommandData data = new RawCommandData();
            data.UserId = userId;
            data.CmdGroup = this.CmdGroup;
            data.Cmd = this.Cmd;
            data.Command = this;
            data.Data = new byte[this.BufferSize];
            data.CreatedTime = DateTime.Now.Ticks;

            Packet.WriteBytes(data.Data, 0, chunkId < 0 ? 0 : chunkId);
            Packet.WriteBytes(data.Data, 4, regionId < 0 ? 0 : regionId);
            Packet.WriteBytes(data.Data, 8, position);
            Packet.WriteBytes(data.Data, 20,rotation);



            return data;
        }

        /// <summary>
        /// Called on receeive data. This is mostly used in server
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerLocationResult ReceiveData(byte[] data, int startIndex, int length)
        {
            if (base.LoadBufferWithIncomingData(data,   startIndex,   length))
            {
                PlayerLocationResult result = new PlayerLocationResult();

                result.UserId = this.ReceiveDataPacket.GetPacketOwner();

                result.ChunkId = this.ReceiveDataPacket.ReadInt();
                result.RegionId = this.ReceiveDataPacket.ReadInt();
                result.Position = this.ReceiveDataPacket.ReadVector3();
                result.Rotation = this.ReceiveDataPacket.ReadQuaternion();

                result.RawData = this.ReceiveDataPacket.GetBufferDataRaw(this.BufferSize);

                CallOnDataReceived(result);

                return result;

            }
           
            return null;
            
        }

        /// <summary>
        /// Calleed in receive data. This is used on client side
        /// </summary>
        /// <param name="data"></param>
        public override void CmdInMultipleDataReceived(RawCommandData data)
        {
           
            PlayerLocationResult result = new PlayerLocationResult();

            result.UserId = data.UserId; 
            result.ChunkId = Packet.ReadInt(data.Data, 0);
            result.RegionId = Packet.ReadInt(data.Data, 4);
            result.Position = Packet.ReadVector3(data.Data, 8);
            result.Rotation = Packet.ReadQuaternion(data.Data, 20);

            CallOnDataReceived(result);

        }



    }
}
