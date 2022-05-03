using System;
using System.Collections;
using System.Numerics;
using LOC_NETWORK.GameItems.Base;
using LOC_NETWORK.GameItems.Base.ItemSettings;
using LOC_NETWORK.Networking;
using LOC_SHARED.GameItems.TypesAndConstants;
using LOC_SHARED.NetworkItems;

namespace LOC_NETWORK.GameItems
{
    public class
        _Player: BaseItem
    {


        public bool IsRiding { get; set; }
        public bool RidingItemId { get; set; }
        public bool RidingItemUID { get; set; }
         
        public bool IsDriving { get; set; }
        public bool DrivingItemId { get; set; }
        public bool DrivingItemUID { get; set; }

        public bool IsBurning { get; set; }
        public bool IsSick { get; set; }
        public bool IsFreezing { get; set; }
        public bool IsWounded { get; set; }

        public int BurnedTimeLeft { get; set; }
        public int SickTimeLeft { get; set; }
        public int FrozenTimeLeft { get; set; }
        public int WoundedTimeLeft { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }


        public int CurrentStamina { get; set; }
        public int MaxStamina { get; set; }

        public int CurrentHunger { get; set; }
        public int MaxHunger { get; set; }

        public int CurrentThirsty { get; set; }
        public int MaxThirsty { get; set; }


        public int CurrentHit { get; set; }
        public int MaxHit { get; set; }

        public int CurrentMana { get; set; }
        public int MaxMana { get; set; }


        public bool UsingWeapon { get; set; }
        public bool UsingRightHand { get; set; }

        public Inventory Inventory { get; set; }


        public Player()
        {
            this.ConnectionReference = null; 
            this.ItemId = ItemIds.Player.NormalPlayer; 
            this.Settings = AllItemSettings.Creature.NormalPlayer; 
        }



        public Player(Hashtable table)
        {

          

            this.ConnectionReference = null;

            int mode = (int)table["Mode"];

            if (mode == 1)
            {
                this.ItemId = ItemIds.Player.NormalPlayer;
            }
            if (mode == 2)
            {
                this.ItemId = ItemIds.Player.AstronautPlayer;
            }
            if (mode == 3)
            {
                this.ItemId = ItemIds.Player.SpaceFighterPlayer;
            }

            this.Settings = AllItemSettings.Creature.NormalPlayer;

            this.IsFlying = (bool)table["IsFlying"]; 
            this.IsRunning = (bool)table["IsRunning"];
            this.IsSwimming = (bool)table["IsSwimming"];
            this.IsWalking = (bool)table["IsWalking"];
            this.IsRiding = (bool)table["IsRiding"];

            this.LocatedPlanet = GameManager.GetPlanet((int)table["PlanetId"]);
            int chunkId = (int)table["ChunkId"];
            int regionId = (int)table["RegionId"];
            if (chunkId > -1)
            {
                this.LocatedChunk = this.LocatedPlanet.FindChunk(chunkId);
                this.NearbyChunks = this.LocatedPlanet.FindNearbyChunks(chunkId);
                this.LocatedChunk.AddPlayer(this);
            }
            if (regionId > -1)
            {
                this.LocatedRegion = this.LocatedPlanet.FindRegion(regionId);
                this.LocatedRegion.AddPlayer(this);
            } 
            this.Position = (Vector3)table["Position"];
            this.Rotation = (Quaternion)table["Rotation"];
            this.UID = (int)table["UID"];
        }


        
        public ConnectedUser ConnectionReference { get; set; }
        public LoggedInUser LoggedInUserReference { get; set; }






    }
}
