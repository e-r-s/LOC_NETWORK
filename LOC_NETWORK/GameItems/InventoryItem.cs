using System;
using LOC_NETWORK.GameItems.Base;

namespace LOC_NETWORK.GameItems
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public int ItemUID { get; set; } 
        public int TotalCount { get; set; }
        public int Index { get; set; }
        public bool ShowOneItemInABox { get; set; }
        public BaseItem Item { get; set; }

    }
}
