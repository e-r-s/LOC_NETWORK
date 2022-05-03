using System;
namespace LOC_NETWORK.GameItems.Creature
{
    public class BaseHumanoid:BaseCreature
    {
        public bool IsRiding { get; set; }
        public bool RidingItemId { get; set; }
        public bool RidingItemUID { get; set; }

        public bool IsDriving { get; set; }
        public bool DrivingItemId { get; set; }
        public bool DrivingItemUID { get; set; }

        //public bool UsingWeapon { get; set; }
        //public bool UsingRightHand { get; set; }
    }
}
