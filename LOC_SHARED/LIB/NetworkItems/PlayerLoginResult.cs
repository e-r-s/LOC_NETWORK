using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics; 
 

namespace LOC_SHARED.NetworkItems
{
    public class PlayerLoginResult
    {
        public int ItemId { get; set; }
        public int UID { get; set; }


        public bool IsFlying { get; set; }
        public bool IsRunning { get; set; }
        public bool IsSwimming { get; set; }
        public bool IsWalking { get; set; }



        public bool UsingWeapon { get; set; }
        public bool UsingRightHand { get; set; } 

        public int PlanetId { get; set; }
        public int ChunkId { get; set; }
        public int RegionId { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public List<EncryptionKey> EncryptionKeys { get; set; }

  


        public byte PacketHeadFirst { get; set; }
        public byte PacketHeadSecond { get; set; }
        public byte PacketTailFirst { get; set; }
        public byte PacketTailSecond { get; set; }


        public bool IsRiding { get; set; }
        public bool RidingItemId { get; set; }
        public bool RidingItemUID { get; set; }

        public bool IsDriving { get; set; }
        public bool DrivingItemId { get; set; }
        public bool DrivingItemUID { get; set; }


        public bool IsPoisoned { get; set; }
        public bool IsBurning { get; set; }
        public bool IsSick { get; set; }
        public bool IsFreezing { get; set; }
        public bool IsWounded { get; set; }

        public int PoisonedTimeLeft { get; set; }
        public int BurnedTimeLeft { get; set; }
        public int SickTimeLeft { get; set; }
        public int FrozenTimeLeft { get; set; }
        public int WoundedTimeLeft { get; set; }

        public int ApplyRecoverTimeLeft { get; set; }
        public int RecoveredTimes { get; set; }

        public int TemperatureLevel { get; set; }
        public int PoisonLevel { get; set; }
        public int SickLevel { get; set; }
         

        public bool IsInvisible { get; set; }
        public bool IsGhost { get; set; }
        public bool IsZombie { get; set; }
        public bool IsAlive { get; set; }
         

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


        public byte PrivateKey { get; set; }
        public string APIKey { get; set; }


        //USEE CLLIENT SIDE LIBRARY FORMAT...


        public int SelectedPrimaryItemIndex { get; set; }
        public int SelectedSecondaryItemIndex { get; set; }

 

        //public InventoryItem[] ClothingItems { get; set; }
        //public InventoryItem[] BackpackItems { get; set; }
        //public InventoryItem[] QuickAccessPrimaryItems { get; set; }
        //public InventoryItem[] QuickAccessSecondaryItems { get; set; }


        //public Inventory Inventory { get; set; }





        //public PlayerLoginResult(Player playerData )
        //{
        //    this.ItemId = playerData.ItemId;
        //    this.UID = playerData.UID;
        //    this.IsFlying = playerData.IsFlying;
        //    this.IsRunning = playerData.IsRunning;
        //    this.IsSwimming = playerData.IsSwimming;
        //    this.IsWalking = playerData.IsWalking;
        //    this.PlanetId = playerData.LocatedPlanet.ItemId; 
        //    this.ChunkId = playerData.LocatedChunk!=null? playerData.LocatedChunk.ItemId : -1;
        //    this.RegionId = playerData.LocatedRegion != null ? playerData.LocatedRegion.ItemId : -1;
        //    this.Position = playerData.Position;
        //    this.Rotation = playerData.Rotation;

        //    this.IsRiding = playerData.IsRiding;
        //    this.RidingItemId = playerData.RidingItemId;
        //    this.RidingItemUID = playerData.RidingItemUID;
        //    this.IsDriving = playerData.IsDriving;
        //    this.DrivingItemId = playerData.DrivingItemId;
        //    this.DrivingItemUID = playerData.DrivingItemUID;
        //    this.IsBurning = playerData.IsBurning;
        //    this.IsSick = playerData.IsSick;
        //    this.IsFreezing = playerData.IsFreezing;
        //    this.IsWounded = playerData.IsWounded;
        //    this.BurnedTimeLeft = playerData.BurnedTimeLeft;
        //    this.SickTimeLeft = playerData.SickTimeLeft;
        //    this.FrozenTimeLeft = playerData.FrozenTimeLeft;
        //    this.WoundedTimeLeft = playerData.WoundedTimeLeft;
        //    this.CurrentHealth = playerData.CurrentHealth;
        //    this.MaxHealth = playerData.MaxHealth;
        //    this.CurrentStamina = playerData.CurrentStamina;
        //    this.MaxStamina = playerData.MaxStamina;
        //    this.CurrentThirsty = playerData.CurrentThirsty;
        //    this.MaxThirsty = playerData.MaxThirsty;

        //    this.CurrentHit = playerData.CurrentHit;
        //    this.MaxHit = playerData.MaxHit;
        //    this.CurrentMana = playerData.CurrentMana;
        //    this.MaxMana = playerData.MaxMana;

        //    this.Inventory = playerData.Inventory;

        //    this.UsingWeapon = playerData.UsingWeapon;
        //    this.UsingRightHand = playerData.UsingRightHand;

        //    //this.SelectedPrimaryItemId = playerData.SelectedPrimaryItemId;
        //    //this.SelectedSecondaryItemId = playerData.ItemId;

        //}

    }
}
