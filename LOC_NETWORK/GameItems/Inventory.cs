using System;
namespace LOC_NETWORK.GameItems
{
    public class Inventory
    {
        public int PlayerUID { get; set; }


        public InventoryItem SelectedPrimaryItem { get; set; }
        public InventoryItem SelectedSecondaryItem { get; set; }

        public InventoryItem[] ClothingItems  { get; set; }
        public InventoryItem[] BackpackItems  { get; set; }
        public InventoryItem[] QuickAccessPrimaryItems  { get; set; }
        public InventoryItem[] QuickAccessSecondaryItems  { get; set; }
         

    }
}
