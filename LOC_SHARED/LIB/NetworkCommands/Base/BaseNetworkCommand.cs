using System;
using System.Numerics;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;

namespace LOC_SHARED.NetworkCommands
{

    public delegate void PacketReceived<T>(T commandResult, Packet packet);

    public class BaseNetworkCommand
    {
       

        protected byte _Cmd;
        /// <summary>
        /// Command Byte value from NetworkCommandType
        /// </summary>
        public byte Cmd { get { return _Cmd; } }

        protected byte _CmdGroup;
        /// <summary>
        /// Command Group Byte value from NetworkCommandType
        /// </summary>
        public byte CmdGroup { get { return _CmdGroup; } }

        protected Packet _SendDataPacket;
        /// <summary>
        /// Packet instance to use send data
        /// </summary>
        public Packet SendDataPacket { get { return _SendDataPacket; } }

        protected Packet _ReceiveDataPacket;
        /// <summary>
        ///  Packet instance to use reeceive data
        /// </summary>
        public Packet ReceiveDataPacket { get { return _ReceiveDataPacket; } }

        protected short _BufferSize = 44; 
        /// <summary>
        /// Buffer size for this command. Only data bytes. Not including header or userId
        /// </summary>
        public short BufferSize { get { return _BufferSize; } }



        protected bool _RequireResponse = false;
        /// <summary>
        /// Whether this command requires response
        /// </summary>
        public bool RequireResponse { get { return _RequireResponse; } }




        /// <summary>
        /// Inits packets
        /// </summary>
        protected void InitPackets()
        {
            _SendDataPacket = new Packet(_CmdGroup, _Cmd, _RequireResponse, _BufferSize, 1);
            _ReceiveDataPacket = new Packet(_CmdGroup, _Cmd, _RequireResponse, _BufferSize,1);
        }

        /// <summary>
        /// Check incoming buffer by cmd and buffer size
        /// </summary>
        /// <param name="buffer">buffer data</param>
        /// <returns></returns>
        public bool CheckBuffer(byte[] buffer)
        {
            if (
                buffer[PacketManager.Constants.BUFFER_INDEX_CMD] == this._Cmd &&
                buffer[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] == this._CmdGroup &&
                buffer.Length == this.BufferSize
              )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sends data packet(under SendDataPacket)  to server.
        /// </summary>
        protected void SendDataPacketToServer()
        { 
            ClientNetworkManager.SendDataToServer(
                    _SendDataPacket.GetBufferEncrypted()
                );
        }

        /// <summary>
        /// Converts data packet to encrypted byte array
        /// </summary>
        /// <returns>Encrypted full data as byte array</returns>
        protected byte[] GetDataEncrypted()
        {
            return _SendDataPacket.GetBufferEncrypted();
               
        }

        /// <summary>
        /// Loads incoming data into ReceiveDAtaPAcket instance
        /// </summary>
        /// <param name="data">incoming data</param>
        /// <returns>whether successfull or not. True for succeessfull</returns>
        protected bool LoadBufferWithIncomingData(byte[] data, int startIndex, int length)
        {
            
            if (_ReceiveDataPacket.LoadBuffer(data,   startIndex,   length))
            {
                return true;
            }

            return false;
        }


        public virtual void CmdInMultipleDataReceived(RawCommandData data)
        {
           
        }
        

    }
}
