using System;
using System.Collections.Generic; 
using System.Threading;
using LiteNetLib;
using LOC_NETWORK.Data;
using LOC_NETWORK.GameItems.Base;
using LOC_NETWORK.GameItems.Creature;
using LOC_NETWORK.Networking;
using LOC_SHARED.GameItems.TypesAndConstants;
using LOC_SHARED.NetworkCommands;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems
{
    public static partial class GameManager
    {
        public static List<Planet> AllPlanets = new List<Planet>();
        public static List<Player> AllPlayers = new List<Player>(); 
        public static List<Player> AllOnlinePlayers = new List<Player>();
        public static List<BaseItem> AllItems = new List<BaseItem>();









        //public static void SendDataToPlayersNearBy(BaseItem item, byte[] data)
        //{

        //    if (item.Location == null)
        //    {
        //        return;
        //    }

        //    if (item.Location.LocatedRegion != null)
        //    {
        //        List<Player> players = item.Location.LocatedPlanet.FindRegion(item.Location.LocatedRegion.ItemId).AllPlayers;
        //        for (int y = 0; y < players.Count; y++)
        //        {
        //            if (players[y].ConnectionReference != null)
        //            {
        //                Networking.NetworkManager.SendDataToUser(players[y].ConnectionReference, data);
        //            }
        //        }
        //        return; 
        //    }



        //   // Chunk[] chunks = item.LocatedPlanet.FindNearbyChunks(item.LocatedChunk.Index);
        //    for (int i = 0; i < item.Location.NearbyChunks.Length; i++)
        //    {
        //        for (int y = 0; y < item.Location.NearbyChunks[i].AllPlayers.Count; y++)
        //        {
        //            if (item.Location.NearbyChunks[i].AllPlayers[y].ConnectionReference != null)
        //            {
        //                Networking.NetworkManager.SendDataToUser(item.Location.NearbyChunks[i].AllPlayers[y].ConnectionReference, data);
        //            }
        //        }
        //    }

        //    //Chunk[] chunks = item.LocatedPlanet.FindNearbyChunks(item.LocatedChunk.Index);
        //    //for (int i = 0; i < chunks.Length; i++)
        //    //{
        //    //    for (int y = 0; y < chunks[i].AllPlayers.Count; y++)
        //    //    {
        //    //        if (chunks[i].AllPlayers[y].ConnectionReference != null)
        //    //        {
        //    //            NetworkManager.SendDataToUser(chunks[i].AllPlayers[y].ConnectionReference, data);
        //    //        }
        //    //    }
        //    //}

        //}



        public static Player GetPlayerByUID(int uid)
        {
            for (int y = 0; y < AllPlayers.Count; y++)
            {
                if (AllPlayers[y].UID==uid)
                {
                    return AllPlayers[y];
                }
            }
            return null;
        }


        private static Thread Thread_CommandCollect;

        private static void InitThreads()
        {
            Thread_CommandCollect = new Thread(new ThreadStart(CollectDataToSendPlayers));
            Thread_CommandCollect.Start();
        }

        public static void Init()
        {
            AllPlanets = new List<Planet>();
            AllPlanets = CacheManager.GetAllPlanets();
            AllItems = CacheManager.GetAllItemsForPlanet(0);
            AllPlayers = CacheManager.GetAllPlayers();

            InitCommands();

            InitThreads();

            NetworkManager.Init();


        } 


        public static PlayerLoginResult PlayerLogedIn(Player p, string ip)
        {
            PlayerLoginResult result = new PlayerLoginResult();
            p.LoggedInUserReference = new LoggedInUser();
            p.LoggedInUserReference.IPAddress = ip;
            p.LoggedInUserReference.APIKey = "SomeConnectionKey";
            p.LoggedInUserReference.LoginTime = DateTime.Now;
            p.LoggedInUserReference.PrivateKey = (byte)(new System.Random().Next(1, 254));
            p.LoggedInUserReference.UID = p.UID;
            
            result.PrivateKey = p.LoggedInUserReference.PrivateKey;
            result.APIKey = p.LoggedInUserReference.APIKey;
            result.UID = p.UID;
            result.EncryptionKeys = NetworkManager.EncryptionKeys;
            result.ChunkId = 1;
            result.RegionId = -1;

            result.PacketHeadFirst = NetworkManager.PacketHeadFirst;
            result.PacketHeadSecond = NetworkManager.PacketHeadSecond; 
            result.PacketTailFirst = NetworkManager.PacketTailFirst; 
            result.PacketTailSecond = NetworkManager.PacketTailSecond;

            return result;
        }


        public static Player PlayerConnected(NetPeer peer, int uid)
        {
           
            Player p = GetPlayerByUID(uid);
            if (p!=null)
            {
                //ConnectedUser user = new ConnectedUser();
                //user.Connection = peer.EndPoint;
                //user.Address = peer.EndPoint.ToString();
                //user.LatestData = new byte[0];
                //user.LatestReceivedData = DateTime.Now;
                //user.peer = peer;
                //user.PlayerData = p;
                //p.ConnectionReference = user;
                

                //user.PrivateKey = new byte[8];
                //System.Random rnd = new System.Random();
                //rnd.NextBytes(user.PrivateKey);

                AllOnlinePlayers.Add(p);

                return p;

            }
            return null; 
        }

        public static bool ItemPositionChanged(BaseCreature item, int chunkId, int regionId, Vector3 position, Quaternion rotation)
        {
            if (chunkId > -1)
            {
                if (item.Location.LocatedChunk != null && item.Location.LocatedChunk.ItemId==chunkId)
                {
                    if (CanMove(item, position))
                    {
                        item.Location.Position = position;
                        item.Location.Rotation = rotation;
                    }
                    return true;
                }
                else
                {
                    return ItemLocatedAreaChanged(item, chunkId, regionId, position, rotation);
                }
            }
           else if (regionId > -1)
            {
                if (item.Location.LocatedRegion != null && item.Location.LocatedRegion.ItemId == regionId)
                {
                    if (CanMove(item, position))
                    {
                        item.Location.Position = position;
                        item.Location.Rotation = rotation;
                    }
                    return true;
                }
                else
                {
                    return ItemLocatedAreaChanged(item, chunkId, regionId, position, rotation);
                }
            }
            else
            {
                return false;
            }
        }

            public static bool ItemLocatedAreaChanged(BaseCreature item, int chunkId, int regionId, Vector3 position, Quaternion rotation)
        {
            BasePlace newArea = null;
            BasePlace previousArea = null;
            

            if (chunkId > -1)
            {
                newArea = item.Location.LocatedPlanet.FindChunk(chunkId);
            }
            if (regionId > -1)
            {
                newArea = item.Location.LocatedPlanet.FindRegion(regionId);
            }
            if (item.Location.LocatedChunk != null)
            {
                previousArea = item.Location.LocatedChunk;
            }
            if (item.Location.LocatedRegion != null)
            {
                previousArea = item.Location.LocatedRegion;
            }

            if(newArea==null || previousArea == null)
            {
                return false;
            }

           

            if (newArea.IsPositionInThisArea(position))
            {
                if (chunkId > -1)
                {
                    item.Location.LocatedChunk = newArea as Chunk;
                    item.Location.NearbyChunks = item.Location.LocatedPlanet.FindNearbyChunks(newArea.ItemId);
                }

                if(item is Player)
                {
                    Player p = item as Player;
                    newArea.AddPlayer(p);
                    previousArea.RemovePlayer(p);
                }
                else
                {
                    newArea.AddItem(item);
                    previousArea.RemoveItem(item); 
                }
                 

                return true;
            }

            return false;
        }

         





        public static Planet GetPlanet(int planetId)
        {
            for (int i = 0; i < AllPlanets.Count; i++)
            {
                if (AllPlanets[i].ItemId == planetId)
                {
                    return AllPlanets[i];

                }
            }
            return null;
        }



        //public static List<BaseItem> GetItemsNearBy(BaseItem item, float distance)
        //{
        //    Planet p = GetPlanet(item.LocatedPlanet.ItemId);
           
        //    if (item.LocatedRegion != null)
        //    {
        //        return p.FindRegion(item.LocatedRegion.ItemId).FindItems(item, distance); 
        //    }

        //    if (item.LocatedChunk != null)
        //    {
        //        Chunk[] chunksNearby = p.FindNearbyChunks(item.LocatedChunk.ItemId);
        //        for(int i=0; i<chunksNearby.Length; i++)
        //        {
        //            chunksNearby[i].AllPlayers
        //                ItemLocationHelper.Distance(AllPlayers[i], item) < distance
        //        }

        //        .FindItems(item, distance);
        //    }
                   
            
        //    return null;
        //}



        public static bool CanInteract( BaseItem item, BaseItem itemInteracted)
        {
            if (item.Settings.CanSelfInteract)
            {
                return ItemLocationHelper.Distance(item, itemInteracted) < item.Settings.MaxInteractionRange;
            }
            if(itemInteracted.Settings.CanSelfInteract)
            {
                return ItemLocationHelper.Distance(item, itemInteracted) < itemInteracted.Settings.MaxInteractionRange;
            }
            return false;
        }



        public static List<BaseItem> FindNPCToAttack(BaseItem item)
        {
            Planet p = GetPlanet(item.Location.LocatedPlanet.ItemId);
            if (item.Location.LocatedRegion != null)
            {
                return p.FindRegion(item.Location.LocatedRegion.ItemId).FindItemsByInteraction(item);
            }

            if (item.Location.LocatedChunk != null)
            {
                return p.FindChunk(item.Location.LocatedChunk.ItemId).FindItemsByInteraction(item);
            }
 
            return null;
        }


        public static void NPCPathFinding(List<BaseItem> NPCs, BaseItem itemToAttack)
        {
            //A* Algorithm call, NCP Commands : Walk, Run, Turn, Jump, JumpForward, Fly, Swim, Set Weeapon, Set Bullet, Attack, Heal, Died, BeeginRide, EndRide, 
        }


        public static bool CanMove(BaseCreature item, Vector3 newPosition)
        {

            float distance = ItemLocationHelper.Distance(item, newPosition);
            if (item.IsWalking && distance > item.Settings.MaxRunSpeedInASecond)
            {
                return false;
            }
            if (item.IsSwimming && distance > item.Settings.MaxSwimSpeedInASecond)
            {
                return false;
            }
            if (item.IsFlying && distance > item.Settings.MaxFlySpeedInASecond)
            {
                return false;
            } 
            return true; 
           
        }


 


    }
}
