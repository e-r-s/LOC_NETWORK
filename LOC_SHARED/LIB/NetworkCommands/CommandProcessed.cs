using System;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;

namespace LOC_SHARED.NetworkCommands
{
    public enum NetworkCommandResult
    {
        CommandReceived = 1,
        CommandProcessedAndApplied = 2,

        CommandProcessingFailed = 3,
        CommandError = 4
    }

    public class CommandProcessResult
    {
        public byte CmdGroup { get; set; }
        public byte Cmd { get; set; }
        public byte Version { get; set; }
        public NetworkCommandResult Result { get; set; }
    }

    public class CommandProcessed : NetworkCommand<CommandProcessResult>
    {

        public CommandProcessed() 
        {
            this._BufferSize = 16;
            this._Cmd = NetworkCommandType.SystemCommands.ReceiveConfirmation;
            this._CmdGroup = NetworkCommandType.SystemCommands.CommandGroup;
            base.InitPackets();
        }

        public void SendData(byte cmdGroup, byte cmd, byte version, NetworkCommandResult result)
        {
            SendDataPacket.WriteBytes(cmdGroup);
            SendDataPacket.WriteBytes(cmd);
            SendDataPacket.WriteBytes(version);
            SendDataPacket.WriteBytes((byte)result);
            base.SendDataPacketToServer();
        }

        public CommandProcessResult ReceiveData(byte[] data, int startIndex, int length)
        {
            if (base.LoadBufferWithIncomingData(data,   startIndex,   length))
            {
                CommandProcessResult result = new CommandProcessResult();
                result.CmdGroup = this.ReceiveDataPacket.ReadByte();
                result.Cmd = this.ReceiveDataPacket.ReadByte();
                result.Version = this.ReceiveDataPacket.ReadByte();
                result.Result = (NetworkCommandResult)this.ReceiveDataPacket.ReadByte();
                CallOnDataReceived(result);
                return result;
            }

            return null;
        }


    }
}
