using System;
using System.Collections.Generic;

namespace LOC_NETWORK.GameItems
{
    public class _LocationChunk
    {
        public List<LocationRegion> AllRegions { get; set; }
        public int ChunkId;


        public LocationRegion FindRegion( int regionId)
        {
            for (int i = 0; i < AllRegions.Count; i++)
            {
                if (AllRegions[i].RegionId == regionId)
                {
                    return AllRegions[i];
                }
            }
            return null;
        }

    }
}
