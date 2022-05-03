using System;
using System.Collections; 
using LOC_NETWORK.GameItems.Base;
using LOC_NETWORK.GameItems.Base.ItemSettings;
using LOC_SHARED.GameItems.TypesAndConstants;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems.Vegetation
{
    public class Tree : BaseVegetation
    {
        public Tree()
        {
        }


        public Tree(Hashtable table)
        {


            this.ItemId = ItemIds.Vegetation.Tree_Pine_Huge;
            this.Settings = AllItemSettings.Vegetation.Tree_Pine_Huge;



            //this.FrozenTimeLeft;
            //this.PoisonedTimeLeft;
            //this.SickTimeLeft;
            //this.WoundedTimeLeft;
            //this.BurnedTimeLeft;

            //this.IsBurning;
            //this.IsFreezing;
            //this.IsPoisoned;
            //this.IsSick;
            //this.IsWounded;

            //this.PoisonLevel;
            //this.SickLevel;
            //this.TemperatureLevel;

            //this.RecoveredTimes;
            //this.ApplyRecoverTimeLeft;





            this.Location.LocatedPlanet = GameManager.GetPlanet((int)table["PlanetId"]);
            this.Location.LocatedPlanetId = this.Location.LocatedPlanet.ItemId;
            int chunkId = (int)table["ChunkId"];
            int regionId = (int)table["RegionId"];
            if (chunkId > -1)
            {
                this.Location.LocatedChunk = this.Location.LocatedPlanet.FindChunk(chunkId);

                this.Location.LocatedChunkId = this.Location.LocatedChunk.ItemId;
                // this.NearbyChunks = this.LocatedPlanet.FindNearbyChunks(chunkId);
                this.Location.LocatedChunk.AddItem(this);
            }
            if (regionId > -1)
            {
                this.Location.LocatedRegion = this.Location.LocatedPlanet.FindRegion(regionId);
                this.Location.LocatedRegionId = this.Location.LocatedRegion.ItemId;
                this.Location.LocatedRegion.AddItem(this);
            }
            this.Location.Position = (Vector3)table["Position"];
            this.Location.Rotation = (Quaternion)table["Rotation"];
            this.UID = (int)table["UID"];
        }


    }
}
