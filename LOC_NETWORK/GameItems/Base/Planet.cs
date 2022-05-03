using System;
using System.Collections.Generic;
using LOC_SHARED.GameItems.TypesAndConstants;

namespace LOC_NETWORK.GameItems.Base
{
    public class Planet: BaseCelestial
    {
        public Planet(int itemId, List<Chunk> chunks, List<Region> regions, int chunkRowSize, int chunkColumnSize, string name)
        {
            this.ItemId = itemId;
            this.UID = itemId;
            this.Name = name;
            this.Chunks = chunks;
            this.Regions = regions;
            this.ChunkColumnSize = chunkColumnSize;
            this.ChunkRowSize = chunkRowSize;

            FillChunkIndexes();


        }

        public void FillChunkIndexes()
        {
            this.ChunkIndexes = new int[this.ChunkRowSize*3 * this.ChunkColumnSize*3];
            int index = 0;
            int chunkIdIndexer = 0;
            int chunkIdIndexerRow = 0;

            for (int c1 = 0; c1 < 3; c1++)
            {
                for (int x = 0; x < this.ChunkColumnSize; x++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        for (int y = 0; y < this.ChunkRowSize; y++)
                        {
                            this.ChunkIndexes[index] = chunkIdIndexer;
                            index++;
                            chunkIdIndexer++;
                        }
                        chunkIdIndexer = chunkIdIndexerRow * this.ChunkRowSize;
                    }
                    chunkIdIndexerRow++;
                    chunkIdIndexer = chunkIdIndexerRow * this.ChunkRowSize;
                }
                chunkIdIndexerRow = 0;
                chunkIdIndexer = chunkIdIndexerRow * this.ChunkRowSize;
            }

        }

        //public string Name { get; set; }
        //public List<Region> Regions { get; set; }
        //public List<Chunk> Chunks { get; set; }
        //public int ChunkRowSize { get; set; }
        //public int ChunkColumnSize { get; set; }

        public Region FindRegion(int regionId)
        {
            for (int i = 0; i < Regions.Count; i++)
            {
                if (Regions[i].ItemId == regionId)
                {
                    return Regions[i];
                }
            }
            return null;
        }


        public Chunk FindChunk(int chunkIndex)
        {
            return Chunks[chunkIndex];

            //for (int i = 0; i < Chunks.Count; i++)
            //{
            //    if (Chunks[i].ItemId == chunkId)
            //    {
            //        return Chunks[i];
            //    }
            //}
            //return null;
        }



        public Chunk[] FindNearbyChunks(int chunkIndex)
        {
            //if (chunkIndex < ChunkRowSize)
            //{
            //    //in first row
            //}
            //else if (chunkIndex < ChunkRowSize * 2)
            //{
            //    //in second row
            //}

            //else

           // int chunkIndex = this.ChunkIndexes[chunkArrayIndex];


            //if (chunkIndex < ChunkRowSize * 3)
            //{

            //    //on top
            //}
            //if (chunkIndex / ChunkRowSize > ChunkColumnSize-4)
            //{
            //    //on bottom
            //}
            //if (chunkIndex% ChunkRowSize < 4)
            //{
            //    //in left
            //}
            //if (chunkIndex % ChunkRowSize > ChunkRowSize-4)
            //{
            //    //in right
            //}
             

            return new Chunk[] {
                Chunks[chunkIndex], //self
                Chunks[chunkIndex - 1], //left;
                Chunks[chunkIndex + 1], //right;

                Chunks[chunkIndex + ChunkRowSize], //under;
                Chunks[chunkIndex + ChunkRowSize + 1], //under right;
                Chunks[chunkIndex + ChunkRowSize - 1], //under left;

                Chunks[chunkIndex - ChunkRowSize], //above;
                Chunks[chunkIndex - ChunkRowSize + 1], //above right;
                Chunks[chunkIndex - ChunkRowSize - 1], //above left;


                
                Chunks[chunkIndex - 2], //left;
                Chunks[chunkIndex - 2 + ChunkRowSize], //left;
                Chunks[chunkIndex - 2 - ChunkRowSize], //left;
                Chunks[chunkIndex + 2], //right;
                Chunks[chunkIndex + 2 + ChunkRowSize], //right;
                Chunks[chunkIndex + 2 - ChunkRowSize], //right;


                Chunks[chunkIndex - (ChunkRowSize*2)], //above;
                Chunks[chunkIndex - (ChunkRowSize*2) + 1], //above right;
                Chunks[chunkIndex - (ChunkRowSize*2) + 2], //above right;
                Chunks[chunkIndex - (ChunkRowSize*2) - 1], //above left;
                Chunks[chunkIndex - (ChunkRowSize*2) - 2], //above left;



                Chunks[chunkIndex + (ChunkRowSize*2)], //under;
                Chunks[chunkIndex + (ChunkRowSize*2) + 1], //under right;
                Chunks[chunkIndex + (ChunkRowSize*2) + 2], //under right;
                Chunks[chunkIndex + (ChunkRowSize*2) - 1], //under left;
                Chunks[chunkIndex + (ChunkRowSize*2) - 2], //under left;


             }; 
           
        }

        private List<BaseItem> GetChunkItems(int chunkIndex)
        {
            if (Chunks[chunkIndex] != null)
            {
                return Chunks[chunkIndex].AllItems;
            }
            else
            {
                return new List<BaseItem>();
            }
        }

        private List<BaseItem> GetMovingChunkItems(int chunkIndex)
        {
            if (Chunks[chunkIndex] != null)
            {
                return Chunks[chunkIndex].AllMovingItems;
            }
            else
            {
                return new List<BaseItem>();
            }
        }


        //public List<BaseItem> FindItemsInNearbyChunks(int chunkIndex)
        //{

        //    List<BaseItem> items = new List<BaseItem>();
             
        //    items.AddRange(GetChunkItems(chunkIndex));
        //    items.AddRange(GetChunkItems(chunkIndex - 1));
        //    items.AddRange(GetChunkItems(chunkIndex + 1));

        //    items.AddRange(GetChunkItems(chunkIndex + ChunkRowSize));
        //    items.AddRange(GetChunkItems(chunkIndex + ChunkRowSize + 1));
        //    items.AddRange(GetChunkItems(chunkIndex + ChunkRowSize - 1));

        //    items.AddRange(GetChunkItems(chunkIndex - ChunkRowSize));
        //    items.AddRange(GetChunkItems(chunkIndex - ChunkRowSize + 1));
        //    items.AddRange(GetChunkItems(chunkIndex - ChunkRowSize - 1));

        //    return items;

        //}




    }
}
