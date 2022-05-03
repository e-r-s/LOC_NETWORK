using System;
using System.Collections.Generic;

namespace LOC_NETWORK.GameItems
{
    public class _LocationRegion
    {
        public List<ItemLocation> AllItems { get; set; }
        public int RegionId;


        public ItemLocation FindItem(int itemUID)
        {
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].UID == itemUID)
                {
                    return AllItems[i];
                }
            }
            return null;
        }



        public List<ItemLocation> FindItemsByInteraction(ItemLocation item)
        {
            List<ItemLocation> foundItems = new List<ItemLocation>();
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (ItemLocationHelper.Distance(AllItems[i], item) < AllItems[i].Settings.MaxInteractionRange)
                {
                    foundItems.Add(AllItems[i]);
                }
            }
            return foundItems;
        }



        public List<ItemLocation> FindItems(ItemLocation item, float distance)
        {
            List<ItemLocation> foundItems = new List<ItemLocation>();
            for (int i = 0; i < AllItems.Count; i++)
            {
                if(ItemLocationHelper.Distance(AllItems[i], item) < distance)
                {
                    foundItems.Add(AllItems[i]);
                } 
            }
            return foundItems;
        }
         

    }
}
