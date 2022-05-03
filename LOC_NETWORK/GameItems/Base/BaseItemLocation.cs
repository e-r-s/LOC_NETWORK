using System;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems.Base
{
    public class BaseItemLocation
    {
        public int[] NearbyChunkIds { get; set; }

        public int LocatedPlanetId { get; set; }
        public int LocatedChunkId { get; set; }
        public int LocatedRegionId { get; set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Size { get; set; }

    }
}
