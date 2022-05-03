using System;
using LOC_SHARED.NetworkItems;

namespace LOC_SHARED.NetworkCommands
{
    public static class AllNetworkCommands
    {

        public static BaseNetworkCommand FindCommand(byte commandGroup, byte command)
        {

            if (commandGroup == NetworkCommandType.PlayerActionCommands.CommandGroup)
            {
                if (command == NetworkCommandType.PlayerActionCommands.PlayerData_Position)
                    return AllNetworkCommands.PlayerActionCommands.PlayerData_Position;

                else if (command == NetworkCommandType.PlayerActionCommands.PlayerData_Aim)
                    return AllNetworkCommands.PlayerActionCommands.PlayerData_Position;

            }
            return null;
        }

        public static void FindAndRunCommand(byte[] data, int startIndex, int length)
        {

            if (data[startIndex + PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] == NetworkCommandType.MainCommands.CommandGroup)
            {
                if (data[startIndex + PacketManager.Constants.BUFFER_INDEX_CMD] == NetworkCommandType.MainCommands.MainCommand_MultipleData)
                    AllNetworkCommands.MainCommands.MainCommand_MultipleData.ReceiveData(data, startIndex, length);
                 
            }
            else if (data[startIndex+PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] == NetworkCommandType.PlayerActionCommands.CommandGroup)
            {
                if (data[startIndex+PacketManager.Constants.BUFFER_INDEX_CMD] == NetworkCommandType.PlayerActionCommands.PlayerData_Position)
                    AllNetworkCommands.PlayerActionCommands.PlayerData_Position.ReceiveData(data,   startIndex,   length);

                else if (data[startIndex+PacketManager.Constants.BUFFER_INDEX_CMD] == NetworkCommandType.PlayerActionCommands.PlayerData_Aim)
                    AllNetworkCommands.PlayerActionCommands.PlayerData_Position.ReceiveData(data,   startIndex,   length);

            } 
        }

         


        public static class MainCommands
        {
            public static MultipleData MainCommand_MultipleData = new MultipleData();
        }
        public static class PlayerActionCommands
        {
            public static PlayerLocationChanged PlayerData_Position = new PlayerLocationChanged();
        }
    }
}
