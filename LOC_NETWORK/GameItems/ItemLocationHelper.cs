using System;
using System.Collections.Generic;
using LOC_NETWORK.GameItems.Base;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems
{
    public class ItemLocationHelper
    {
        public ItemLocationHelper()
        {
        }

        public static float Distance(BaseItem first, BaseItem second)
        {
            return Vector3.Distance(first.Location.Position, second.Location.Position);
        }

        public static float Distance(BaseItem first, Vector3 location)
        {
            return Vector3.Distance(first.Location.Position, location);
        }

        public static bool IsCollided(BaseItem first, BaseItem second)
        {
            return false;
        }



 


    }
}
