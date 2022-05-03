using System;
using System.Numerics;

namespace LOC_NETWORK.GameItems.Base
{
    public class ItemLocation: BaseItemLocation
    {

        public Chunk[] NearbyChunks { get; set; } 
        public Planet LocatedPlanet { get; set; }
        public Chunk LocatedChunk { get; set; }
        public Region LocatedRegion { get; set; }


    }
}
