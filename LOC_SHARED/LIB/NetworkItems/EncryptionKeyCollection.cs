using System;
using System.Collections.Generic;

namespace LOC_SHARED.NetworkItems
{
    public class EncryptionKeyCollection
    {
        public EncryptionKeyCollection()
        {
            AllKeys = new List<EncryptionKey>();
            counter = 0;
        }
        public List<EncryptionKey> AllKeys { get; set; }
        int counter = 0;


        public EncryptionKey CurrentKey;

        public void ReloadAllKeys(List<EncryptionKey> keys)
        {
            this.AllKeys = keys;
            counter = 0;

            for (int i = 1; i < 11; i++)
            {
                AllKeys[i].IsActive = true;
            }

            AllKeys[11].IsActive = false;
            AllKeys[12].IsActive = false;
            AllKeys[0].IsActive = false;
        }

        public EncryptionKey SetCurrentKey()
        {
            if (counter > 10)
            {
                counter = 0;
            }
            counter++;
            CurrentKey = AllKeys[counter];
            return CurrentKey;
        }



 


        public void NewKeyReceived(EncryptionKey key)
        {
            AllKeys.Add(key);
            AllKeys.RemoveAt(0);

            AllKeys[10].IsActive = true;

            AllKeys[11].IsActive = false;
            AllKeys[12].IsActive = false;
            AllKeys[0].IsActive = false;
 
        }


        public EncryptionKey FindEncryptionKey(byte id)
        { 
            for(int i=0; i<AllKeys.Count; i++)
            {
                if (AllKeys[i].Id == id)
                {
                    return AllKeys[i];
                } 
            }
            return null;
        }


    }
}
