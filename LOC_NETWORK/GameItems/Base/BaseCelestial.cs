using System;
using System.Collections.Generic;

namespace LOC_NETWORK.GameItems.Base
{
    public class BaseCelestial
    {


        public int UID { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public List<Region> Regions { get; set; }
        public List<Chunk> Chunks { get; set; }
        public int ChunkRowSize { get; set; }
        public int ChunkColumnSize { get; set; }

        public int[] ChunkIndexes { get; set; }

        public BaseCelestial()
        {
        }
    }
}
