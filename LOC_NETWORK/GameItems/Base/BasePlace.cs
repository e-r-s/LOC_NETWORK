using System;
using System.Collections.Generic; 
using LOC_NETWORK.GameItems.Creature;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems.Base
{
    public class BasePlace
    {
        public BasePlace()
        {
            AllItems = new List<BaseItem>();
            AllMovingItems = new List<BaseItem>();
            AllPlayers = new List<Player>();

            this.Location = new ItemLocation();
        }


        public int UID { get; set; }
        public int ItemId { get; set; }


        public ItemLocation Location { get; set; } 

        public List<BaseItem> AllItems { get; set; }
        public List<BaseItem> AllMovingItems { get; set; }
        public List<Player> AllPlayers { get; set; }

         
        public bool AddItem(BaseItem item)
        {
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].UID == item.UID)
                {
                    return false;
                }
            }

            AllItems.Add(item);
            if (item.Settings.CanMove)
            {
                AllMovingItems.Add(item);
            }
            return true;
        }


        public bool AddPlayer(Player item)
        {
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].UID == item.UID)
                {
                    return false;
                }
            }

            AllPlayers.Add(item);
            AllMovingItems.Add(item);
            AllItems.Add(item);

            return true;
        }





        public bool RemoveItem(BaseItem item)
        {
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].UID == item.UID)
                {
                    if (item.Settings.CanMove)
                    {
                        AllMovingItems.Remove(AllItems[i]);
                    }

                    AllItems.RemoveAt(i);

                    return true;
                }
            }

            return false;
        }


        public bool RemovePlayer(Player item)
        {
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].UID == item.UID)
                {
                    AllMovingItems.Remove(AllItems[i]);
                    AllPlayers.Remove(AllItems[i] as Player);
                    AllItems.Remove(AllItems[i]);
                    return true;
                }
            }
            return false;
        }




        public BaseItem FindItem(int itemUID)
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



        public List<BaseItem> FindItemsByInteraction(BaseItem item)
        {
            List<BaseItem> foundItems = new List<BaseItem>();
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (AllItems[i].Settings.CanSelfInteract && ItemLocationHelper.Distance(AllItems[i], item) < AllItems[i].Settings.MaxInteractionRange)
                {
                    foundItems.Add(AllItems[i]);
                }
            }
            return foundItems;
        }



        public List<BaseItem> FindItems(BaseItem item, float distance)
        {
            List<BaseItem> foundItems = new List<BaseItem>();
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (ItemLocationHelper.Distance(AllItems[i], item) < distance)
                {
                    foundItems.Add(AllItems[i]);
                }
            }
            return foundItems;
        }

        public bool IsPositionInThisArea(Vector3 position)
        {

            if (this.Location.Position.x <= position.x &&
            this.Location.Position.x + this.Location.Size.x >= position.x &&
            this.Location.Position.z <= position.z &&
            this.Location.Position.z + this.Location.Size.z >= position.z)
            {
                return true;
            }
            return false;
        }




        public List<Player> FindPlayers(BaseItem item, float distance)
        {
            List<Player> foundItems = new List<Player>();
            for (int i = 0; i < AllPlayers.Count; i++)
            {
                if (ItemLocationHelper.Distance(AllPlayers[i], item) < distance)
                {
                    foundItems.Add(AllPlayers[i]);
                }
            }
            return foundItems;
        }


    }
}
