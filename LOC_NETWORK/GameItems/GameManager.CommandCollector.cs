using System;
using System.Collections.Generic;
using LOC_NETWORK.GameItems.Creature;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkCommands.Base;

namespace LOC_NETWORK.GameItems
{
    public static partial class GameManager
    {
         

        private static List<int> UserIdsWhoAlreadyReceivedOtherPlayersData = new List<int>();

        private static byte[] CollectDataFromNearbyChunks(Player p)
        {

            if (p == null)
            {
                return null;
            }

            short totalDataLength = 0;
            List<RawCommandData> allDataToSend = new List<RawCommandData>();
            for (int i = 0; i < p.Location.NearbyChunks.Length; i++)
            {
                for (int y = 0; y < p.Location.NearbyChunks[i].AllPlayers.Count; y++)
                {
                    if (p.Location.NearbyChunks[i].AllPlayers[y] == null || p.ConnectionReference == null)
                    {
                        continue;
                    }
                    totalDataLength += p.Location.NearbyChunks[i].AllPlayers[y].GetActionData(p.ConnectionReference.LatestSentData.Ticks, ref allDataToSend);

                }
            }
            if (totalDataLength == 0)
            {
                return null;
            }
            MultipleDataResult dataToSend = new MultipleDataResult();
            dataToSend.ChildCommandData = allDataToSend.ToArray();
            dataToSend.TotalDataSize = totalDataLength;

            return AllNetworkCommands.MainCommands.MainCommand_MultipleData.GetDataEncrypted(dataToSend);


            //Networking.NetworkManager.SendDataToUser(p.ConnectionReference, readyToSend);
        }


        private static byte[] CollectDataFromSameChunk(Player p)
        {

            //if (UserIdsWhoAlreadyReceivedOtherPlayersData.Contains(p.UID))
            //{
            //    return;
            //}
            if (p == null)
            {
                return null; 
            }
            short totalDataLength = 0;
            List<RawCommandData> allDataToSend = new List<RawCommandData>();

            for (int y = 0; y < p.Location.LocatedChunk.AllPlayers.Count; y++)
            {
                if (p.Location.LocatedChunk.AllPlayers[y] == null || p.ConnectionReference==null)
                {
                    continue;
                }
                totalDataLength += p.Location.LocatedChunk.AllPlayers[y].GetActionData(p.ConnectionReference.LatestSentData.Ticks, ref allDataToSend);
            }
            if (totalDataLength == 0)
            {
                return null;
            }

            MultipleDataResult dataToSend = new MultipleDataResult();
            dataToSend.ChildCommandData = allDataToSend.ToArray();
            dataToSend.TotalDataSize = totalDataLength;

            return AllNetworkCommands.MainCommands.MainCommand_MultipleData.GetDataEncrypted(dataToSend);



            //for (int i = 0; i < p.Location.LocatedChunk.AllPlayers.Count; i++)
            //{
            //    UserIdsWhoAlreadyReceivedOtherPlayersData.Add(p.Location.LocatedChunk.AllPlayers[i].UID);
            //    Networking.NetworkManager.SendDataToUser(p.Location.LocatedChunk.AllPlayers[i].ConnectionReference, readyToSend);
            //}


        }

        private static byte[] CollectDataFromSameRegion(Player p)
        {


            //if (UserIdsWhoAlreadyReceivedOtherPlayersData.Contains(p.UID))
            //{
            //    return;
            //}
            short totalDataLength = 0;
            List<RawCommandData> allDataToSend = new List<RawCommandData>();

            for (int y = 0; y < p.Location.LocatedRegion.AllPlayers.Count; y++)
            {
                totalDataLength += p.Location.LocatedRegion.AllPlayers[y].GetActionData(p.ConnectionReference.LatestSentData.Ticks, ref allDataToSend);
            }

            MultipleDataResult dataToSend = new MultipleDataResult();
            dataToSend.ChildCommandData = allDataToSend.ToArray();
            dataToSend.TotalDataSize = totalDataLength;
            byte[] readyToSend = AllNetworkCommands.MainCommands.MainCommand_MultipleData.GetDataEncrypted(dataToSend);

            //for(int i=0; i< p.Location.LocatedRegion.AllPlayers.Count; i++)
            //{ 
            //    UserIdsWhoAlreadyReceivedOtherPlayersData.Add(p.Location.LocatedRegion.AllPlayers[i].UID);
            //    Networking.NetworkManager.SendDataToUser(p.Location.LocatedRegion.AllPlayers[i].ConnectionReference, readyToSend);
            //}
            return AllNetworkCommands.MainCommands.MainCommand_MultipleData.GetDataEncrypted(dataToSend);


        }

        private static void CollectDataToSendPlayers()
        {
            while (true)
            {
                if(AllOnlinePlayers==null || AllOnlinePlayers.Count == 0)
                {
                    continue;
                }

                for (int i = 0; i < AllOnlinePlayers.Count; i++)
                {

                    if (AllOnlinePlayers[i]==null || UserIdsWhoAlreadyReceivedOtherPlayersData.Contains(AllOnlinePlayers[i].UID))
                    {
                        continue;
                    }

                    if (AllOnlinePlayers[i].Location.LocatedChunk != null)
                    {
                        byte[] dataFromNearByChunks = CollectDataFromNearbyChunks(AllOnlinePlayers[i]);
                        byte[] dataFromSameChunk = CollectDataFromSameChunk(AllOnlinePlayers[i]);

                        for (int p = 0; p < AllOnlinePlayers[i].Location.LocatedChunk.AllPlayers.Count; p++)
                        {
                            UserIdsWhoAlreadyReceivedOtherPlayersData.Add(AllOnlinePlayers[i].Location.LocatedChunk.AllPlayers[p].UID);
                            if (dataFromNearByChunks != null)
                            {
                                Networking.NetworkManager.SendDataToUser(AllOnlinePlayers[i].Location.LocatedChunk.AllPlayers[p].ConnectionReference, dataFromNearByChunks);
                            }
                            if (dataFromSameChunk != null)
                            {
                                Networking.NetworkManager.SendDataToUser(AllOnlinePlayers[i].Location.LocatedChunk.AllPlayers[p].ConnectionReference, dataFromSameChunk);
                            }
                        }

                    }
                    if (AllOnlinePlayers[i].Location.LocatedRegion != null)
                    {
                        byte[] dataFromSameRegion = CollectDataFromSameRegion(AllOnlinePlayers[i]);

                        for (int p = 0; p < AllOnlinePlayers[i].Location.LocatedRegion.AllPlayers.Count; p++)
                        {
                            UserIdsWhoAlreadyReceivedOtherPlayersData.Add(AllOnlinePlayers[i].Location.LocatedRegion.AllPlayers[p].UID);
                            Networking.NetworkManager.SendDataToUser(AllOnlinePlayers[i].Location.LocatedRegion.AllPlayers[p].ConnectionReference, dataFromSameRegion);
                        }
                    }
                }

                UserIdsWhoAlreadyReceivedOtherPlayersData = new List<int>();
            }
        }


    }
}
