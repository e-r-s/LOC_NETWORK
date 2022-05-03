using System;
using System.Numerics;
using LOC_NETWORK.GameItems.Base;

namespace LOC_NETWORK.GameItems
{
    public class _ItemLocation
    {
        public _ItemLocation()
        {
        }

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Size;
        public bool IsWallking;
        public bool IsFlying;
        public bool IsSwimming;

        public int ItemId;
        public int UID;


        //public bool CanWalk;
        //public bool CanSwim;
        //public bool CanFly;

        //public float MaxRunSpeedInASecond;
        //public float MaxFlySpeedInASecond;
        //public float MaxSwimSpeedInASecond;

        //public float MaxInteractionRange;

    }
}
