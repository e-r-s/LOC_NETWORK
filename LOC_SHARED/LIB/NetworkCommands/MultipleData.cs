using System;
using System.Numerics;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;

namespace LOC_SHARED.NetworkCommands
{

    public class MultipleDataResult
    {
        public RawCommandData[] ChildCommandData { get; set; }
        public short TotalDataSize { get; set; }
    }
      

    public class MultipleData : NetworkCommand<MultipleDataResult>
    {

        public MultipleData()
        {
            this._BufferSize = 44;
            this._Cmd = NetworkCommandType.MainCommands.MainCommand_MultipleData;
            this._CmdGroup = NetworkCommandType.MainCommands.CommandGroup;
            this.InitPackets();
        }



        protected new void InitPackets()
        {
            _SendDataPacket = new Packet();
            _ReceiveDataPacket = new Packet();
        }


        public byte[] GetDataEncrypted(MultipleDataResult multipleData)
        {

            short totalSize = (short)(multipleData.TotalDataSize + (multipleData.ChildCommandData.Length * 6));

            _SendDataPacket.SetPacketContent(this.CmdGroup, this.Cmd, this.RequireResponse, totalSize, (byte)multipleData.ChildCommandData.Length);

            for (int i = 0; i < multipleData.ChildCommandData.Length; i++)
            {
                SendDataPacket.WriteBytes(multipleData.ChildCommandData[i].UserId);
                SendDataPacket.WriteBytes(multipleData.ChildCommandData[i].CmdGroup);
                SendDataPacket.WriteBytes(multipleData.ChildCommandData[i].Cmd);

                for (int y = 0; y < multipleData.ChildCommandData[i].Data.Length; y++)
                {
                    SendDataPacket.WriteBytes(multipleData.ChildCommandData[i].Data[y]);
                } 
            }
            return base.GetDataEncrypted();
           // base.SendDataPacketToServer();
        }


        public MultipleDataResult ReceiveData(byte[] data, int startIndex, int length)
        {
            if (base.LoadBufferWithIncomingData(data,   startIndex,   length))
            {
                byte totalDataInBuffer = this.ReceiveDataPacket.GetBufferDataCount();

                MultipleDataResult result = new MultipleDataResult(); 
                result.ChildCommandData = new RawCommandData[totalDataInBuffer];

                for (int i = 0; i < totalDataInBuffer; i++)
                {
                    RawCommandData cmdData = new RawCommandData();
                    cmdData.UserId = this.ReceiveDataPacket.ReadInt();

                    //Ignore data belongs to self
                    if(cmdData.UserId == PacketManager.CurrentUserId)
                    {
                       // continue; 
                    }
                    
                    cmdData.CmdGroup = this.ReceiveDataPacket.ReadByte();
                    cmdData.Cmd = this.ReceiveDataPacket.ReadByte();
                    BaseNetworkCommand cmdInstance = AllNetworkCommands.FindCommand(cmdData.CmdGroup, cmdData.Cmd);
                    cmdData.Data = this.ReceiveDataPacket.ReadBytes(cmdInstance.BufferSize);
                    cmdData.Command = cmdInstance;
                    

                    result.ChildCommandData[i] = cmdData;
                    result.TotalDataSize += cmdInstance.BufferSize;

                    cmdInstance.CmdInMultipleDataReceived(cmdData);
                }
                CallOnDataReceived(result);

                return result;

            }

            return null;

        }


    }
}
