using System; 
using System.Threading;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.Util; 

namespace LOC_SHARED.NetworkItems
{
    public static class _NetworkManager
    {

        public static void Init()
        {
            //ClientNetworkManager.Init();
        }

        public static class Location
        {
            //public static void PlayerPositionChanged(Vector3 position, Quaternion rotation)
            //{
            //    //AllNetworkCommands.PlayerActionCommands.PlayerData_Position.SendData(  position,   rotation);
            //    //AllNetworkCommands.PlayerActionCommands.PlayerData_Position.OnDataReceived += delegate (PlayerLocationResult commandResult, Packet packet)
            //    //{
                   
            //    //};

            //    //new PacketReceived<PlayerLocationResult>(){};
            //    //PacketManager.PlayerLocationPacket.WriteBytes(position);
            //    //PacketManager.PlayerLocationPacket.WriteBytes(rotation);

            //    //ClientNetworkManager.SendDataToServer(
            //    //        PacketManager.PlayerLocationPacket.GetBufferEncrypted()
            //    //    );
            //}
             
        }

        public static class Inventory
        {
            public static void ChangeSelectedPrimaryItem(int index, int itemId)
            {


            }

            public static void ChangeSelectedSecondaryItem(int index, int itemId)
            {

            }

            public static void SetQuickAccessPrimaryItemFromPrimaryItem(int sourceIndex, int itemId, int targetIndex)
            {

            }

            public static void SetQuickAccessPrimaryItemFromBackPack(int sourceIndex, int itemId, int targetIndex)
            {

            }

            public static void ReplaceQuickAccessPrimaryItemFromPrimaryItem(int sourceIndex, int itemId, int targetIndex)
            {

            }
            public static void ReplaceQuickAccessPrimaryItemFromBackPack(int sourceIndex, int itemId, int targetIndex)
            {

            }

        }


    }
}
