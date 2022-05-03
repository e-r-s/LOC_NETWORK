using System;
using LOC_NETWORK.GameItems.Base;

namespace LOC_NETWORK.GameItems.Creature
{
    public class BaseCreature :BaseLiving
    {
        public bool IsWalking { get; set; }
        public bool IsRunning { get; set; }
        public bool IsFlying { get; set; }
        public bool IsSwimming { get; set; }


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

        public bool IsInvisible { get; set; }
        public bool IsGhost { get; set; }
        public bool IsZombie { get; set; }
        public bool IsAlive { get; set; }

        //public int ApplyRecoverTimeLeft { get; set; }
        //public int RecoveredTimes { get; set; }

        public Inventory Inventory { get; set; }

    }
}
