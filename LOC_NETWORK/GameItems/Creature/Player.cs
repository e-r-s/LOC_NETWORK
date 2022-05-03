using System;
using System.Collections;
using System.Collections.Generic; 
using LOC_NETWORK.GameItems.Base;
using LOC_NETWORK.GameItems.Base.ItemSettings;
using LOC_NETWORK.NetworkCommands;
using LOC_NETWORK.Networking;
using LOC_SHARED.GameItems.TypesAndConstants;
using LOC_SHARED.NetworkCommands.Base;
using LOC_SHARED.NetworkItems;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems.Creature
{
    public class Player:BaseHumanoid
    {
        public Player()
        { 
            this.ConnectionReference = null;
            this.ItemId = ItemIds.Player.NormalPlayer;
            this.Settings = AllItemSettings.Creature.NormalPlayer;
            this.Location = new ItemLocation();
            this._ReceivedData = new RawCommandData[21];
            
        }

        public Player(Hashtable table) : this()
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

            this.Location.LocatedPlanet = GameManager.GetPlanet((int)table["PlanetId"]);
            int chunkId = (int)table["ChunkId"];
            int regionId = (int)table["RegionId"];
            if (chunkId > -1)
            {
                this.Location.LocatedChunk = this.Location.LocatedPlanet.FindChunk(chunkId);
                this.Location.NearbyChunks = this.Location.LocatedPlanet.FindNearbyChunks(chunkId);
                this.Location.LocatedChunk.AddPlayer(this);
            }
            if (regionId > -1)
            {
                this.Location.LocatedRegion = this.Location.LocatedPlanet.FindRegion(regionId);
                this.Location.LocatedRegion.AddPlayer(this);
            }
            this.Location.Position = (Vector3)table["Position"];
            this.Location.Rotation = (Quaternion)table["Rotation"];
            this.UID = (int)table["UID"];
        }


        public PlayerLoginResult GetPlayerLoginResult()
        {
            PlayerLoginResult result = new PlayerLoginResult();

            result.ItemId = this.ItemId;
            result.UID = this.UID;

            result.IsFlying = this.IsFlying;
            result.IsRunning = this.IsRunning;
            result.IsSwimming = this.IsSwimming;
            result.IsWalking = this.IsWalking;

            result.PlanetId = this.Location.LocatedPlanet.ItemId;
            result.ChunkId = this.Location.LocatedChunk != null ? this.Location.LocatedChunk.ItemId : -1;
            result.RegionId = this.Location.LocatedRegion != null ? this.Location.LocatedRegion.ItemId : -1;
            result.Position = new System.Numerics.Vector3(this.Location.Position.x, this.Location.Position.y, this.Location.Position.z);
            result.Rotation = new System.Numerics.Quaternion(this.Location.Rotation.x, this.Location.Rotation.y, this.Location.Rotation.z, this.Location.Rotation.w);

            result.IsRiding = this.IsRiding;
            result.RidingItemId = this.RidingItemId;
            result.RidingItemUID = this.RidingItemUID;

            result.IsDriving = this.IsDriving;
            result.DrivingItemId = this.DrivingItemId;
            result.DrivingItemUID = this.DrivingItemUID;

            result.IsPoisoned = this.IsPoisoned;
            result.IsBurning = this.IsBurning;
            result.IsSick = this.IsSick;
            result.IsFreezing = this.IsFreezing;
            result.IsWounded = this.IsWounded;

            result.PoisonedTimeLeft = this.PoisonedTimeLeft;
            result.BurnedTimeLeft = this.BurnedTimeLeft;
            result.SickTimeLeft = this.SickTimeLeft;
            result.FrozenTimeLeft = this.FrozenTimeLeft;
            result.WoundedTimeLeft = this.WoundedTimeLeft;

            result.ApplyRecoverTimeLeft = this.ApplyRecoverTimeLeft;
            result.RecoveredTimes = this.RecoveredTimes;

            result.TemperatureLevel = this.TemperatureLevel;
            result.PoisonLevel = this.PoisonLevel;
            result.SickLevel = this.SickLevel;

            result.IsInvisible = this.IsInvisible;
            result.IsGhost = this.IsGhost;
            result.IsZombie = this.IsZombie;
            result.IsAlive = this.IsAlive;


            result.CurrentHealth = this.CurrentHealth;
            result.MaxHealth = this.MaxHealth;

            result.CurrentStamina = this.CurrentStamina;
            result.MaxStamina = this.MaxStamina;

            result.CurrentThirsty = this.CurrentThirsty;
            result.MaxThirsty = this.MaxThirsty;

            result.CurrentHunger = this.CurrentHunger;
            result.MaxHunger = this.MaxHunger;

            result.CurrentHit = this.CurrentHit;
            result.MaxHit = this.MaxHit;

            result.CurrentMana = this.CurrentMana;
            result.MaxMana = this.MaxMana;

            result.UsingWeapon = this.UsingWeapon;
            result.UsingRightHand = this.UsingRightHand;

            result.SelectedPrimaryItemIndex = this.Inventory.SelectedPrimaryItem.Index;
            result.SelectedSecondaryItemIndex = this.Inventory.SelectedSecondaryItem.Index;
            //TODO ALLL inventory


            return result;
             

        }




        public ConnectedUser ConnectionReference { get; set; }
        public LoggedInUser LoggedInUserReference { get; set; }



        private int actionDataIndex = 0;
        public void AddNewActionData(RawCommandData data)
        {
            _ReceivedData[actionDataIndex] = data;
            actionDataIndex++;
            if (actionDataIndex > 20)
            {
                actionDataIndex = 0;
            }
        }

        public short GetActionData(long startFromTime, ref List<RawCommandData> destinationList)
        {
            short dataLength = 0;

            if (_ReceivedData == null)
            {
                LOC_SHARED.Util.Logger.Log("dada");
            }
           
             for (int i=0; i< _ReceivedData.Length; i++)
            {
                if (ReceivedData[i] == null)
                {
                    continue;
                }
                if(ReceivedData[i].CreatedTime> startFromTime)
                {
                    destinationList.Add(ReceivedData[i]);
                    dataLength += ReceivedData[i].Command.BufferSize;
                }
            }
            return dataLength;
        }


        private RawCommandData[] _ReceivedData;
        public RawCommandData[] ReceivedData { get { return _ReceivedData;  }   }

    }
}
