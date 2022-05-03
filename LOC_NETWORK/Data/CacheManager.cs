using System;
using System.Collections;
using System.Collections.Generic; 
using LOC_NETWORK.GameItems;
using LOC_NETWORK.GameItems.Base;
using LOC_NETWORK.GameItems.Creature;
using LOC_NETWORK.GameItems.Vegetation;
using LOC_SHARED.GameItems.TypesAndConstants;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.Data
{
    public static class CacheManager
    {


        public static List<Planet> GetAllPlanets()
        {
            List<Planet> result = new List<Planet>();

            List<Chunk> chunks = new List<Chunk>();
            List<Region> regions = new List<Region>();

            int index = 0;
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    Chunk chunk = new Chunk();
                    chunk.ItemId = index;
                    chunk.Location.Position = new Vector3(x*100, 0, y*100);
                    chunk.Location.Rotation = Quaternion.identity;
                    chunk.Location.Size = new Vector3(100, 100, 100);
                    chunk.UID = index;
                    chunks.Add(chunk);
                    index++;
                }
            }


            Region region = new Region();
            region.ItemId = 0;
            region.Location.Position = new Vector3(0, 0, 0);
            region.Location.Rotation = Quaternion.identity;
            region.Location.Size = new Vector3(100, 100, 100);
            region.UID = 0;

            regions.Add(region);
             
            result.Add(new Planet(ItemIds.Celestial.Planet_Earth, chunks, regions, 100, 100, "Planet Earth"));
            result.Add(new Planet(ItemIds.Celestial.Planet_Mars, chunks, regions, 100, 100, "Planet Mars"));

            return result;
        }


        public static List<BaseItem> GetAllItemsForPlanet(int planetId)
        {
            List<BaseItem> result = new List<BaseItem>();
            Hashtable table = new Hashtable();
            table["Size"] = 1;
            table["Type"] = 1;
            table["PlanetId"] = 1;
            table["ChunkId"] = 1010;
            table["RegionId"] = -1;
            table["Position"] = new Vector3(1000, 0, 1020);
            table["Rotation"] = Quaternion.identity;
            table["UID"] = 1;

            result.Add(new Tree(table));
            return result;

        }

        public static List<Player> GetAllPlayers()
        {
            List<Player> result = new List<Player>();
            Hashtable table = new Hashtable();
            table["Mode"] = 1;
            table["IsFlying"] = false;
            table["IsRunning"] = false;
            table["IsSwimming"] = false;
            table["IsWalking"] = false;
            table["PlanetId"] = 1;
            table["ChunkId"] = 1010;
            table["RegionId"] = -1;
            table["IsRiding"] = false;
            table["Position"] = new Vector3(1000, 0, 1000);
            table["Rotation"] = Quaternion.identity;
            table["UID"] = 1;
            
            result.Add(new Player(table));
            return result;

        }
         
    }
}
