using System;
using System.Collections.Generic;
using System.Numerics;
using LOC_NETWORK.GameItems.Base;

namespace LOC_NETWORK.GameItems.Base
{
    public class BaseItem 
    {
        public BaseItem()
        {
            
        }

        //public bool IsWalking { get; set; }
        //public bool IsRunning { get; set; }
        //public bool IsFlying { get; set; }
        //public bool IsSwimming { get; set; }
          
        public ItemSetting Settings { get; set; }
        public ItemLocation Location { get; set; }
        public int UID { get; set; } 
        public int ItemId { get; set; }


        //public Chunk[] NearbyChunks { get; set; }
        //public Planet LocatedPlanet { get; set; }
        //public Chunk LocatedChunk { get; set; }
        //public Region LocatedRegion { get; set; }

    }
}
