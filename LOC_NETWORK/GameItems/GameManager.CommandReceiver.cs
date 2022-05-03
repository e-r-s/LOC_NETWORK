using System;
using LOC_NETWORK.GameItems.Creature;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.GameItems
{
    public static partial class GameManager
    {

         
        private static void InitCommands()
        {
            AllNetworkCommands.PlayerActionCommands.PlayerData_Position.OnDataReceived += PlayerData_Position_OnDataReceived;
        }

        private static void PlayerData_Position_OnDataReceived(PlayerLocationResult commandResult, Packet packet)
        {
            Player p = GameManager.GetPlayerByUID(commandResult.UserId);

            if(GameManager.ItemPositionChanged(p, commandResult.ChunkId, commandResult.RegionId, commandResult.Position, commandResult.Rotation))
            {
                RawCommandData dataToSend = AllNetworkCommands.PlayerActionCommands.PlayerData_Position.GetRawCommandData(commandResult);
                p.AddNewActionData(dataToSend); 
            } 
        }







    }
}
