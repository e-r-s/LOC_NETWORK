using System;
using System.Collections.Generic;

namespace LOC_SHARED.NetworkItems
{

    public delegate void InitializedDelegate();

    public static class PacketManager
    {

        public static class Constants
        {
             

            public static int BUFFER_INDEX_HEAD_FIRST = 0;
            public static int BUFFER_INDEX_HEAD_SECOND = 1;
            public static int BUFFER_INDEX_TAIL_FIRST = 2;
            public static int BUFFER_INDEX_TAIL_SECOND = 1;

            public static int BUFFER_INDEX_BUFFER_SIZE1 = 2;
            public static int BUFFER_INDEX_BUFFER_SIZE2 = 3;

            public static int BUFFER_INDEX_ENCRYPTION_KEY_ID = 4;

            public static int BUFFER_INDEX_CMD = 5;
            public static int BUFFER_INDEX_CMD_GROUP = 6;
            public static int BUFFER_INDEX_DATA_VERSION = 7; // change to packet version


            public static int BUFFER_INDEX_DATA_COUNT = 8;

            /// <summary>
            /// Whether CMD require response is defined in this buffer index
            /// </summary>
            public static int BUFFER_INDEX_NEEED_RESPONSE = 9;

            public static int BUFFER_INDEX_USER_PRIVATE_KEY = 10; //only for send


            public static int BUFFER_INDEX_USERID1 = 11;
            public static int BUFFER_INDEX_USERID2 = 12;
            public static int BUFFER_INDEX_USERID3 = 13;
            public static int BUFFER_INDEX_USERID4 = 14;

            //Encryption start from this index. Head bytes and key. TODO: head+pkey+keyid+cmdg+cmd. new value 6.  
            public static int BUFFER_INDEX_ENCRYPTION_START_RESERVED = 8;
            //Encryption continue until last xx index . Reserved at the end. Tail bytes.
            public static int BUFFER_INDEX_ENCRYPTION_END_RESERVED = 2;

            /// <summary>
            /// After which index CMD data starts from.
            /// </summary>
            public static int BUFFER_INDEX_CMD_DATA_START = 15; 
            //public static int BUFFER_INDEX_NONENCRYPTED_DATA_START = 2;
        }

        //We convert userid into bytes only once
        public static int CurrentUserId = 0;
        public static byte[] CurrentUserIdBytes = new byte[4];

        //Each user has a provate key added into packet.
        public static byte CurrentUserPrivateKey = 0;

        //All clients using 10 keys. This keys are public. Key index is added into each packet.
        private static EncryptionKeyCollection _EncryptionKeys = new EncryptionKeyCollection();
        public static EncryptionKey CurrentEncryptionKey { get { return _EncryptionKeys.CurrentKey;  } }

        //Each packet may have head and tail to know begining and the end of the packet.  
        public static byte PacketHeadFirst;
        public static byte PacketHeadSecond;
        public static byte PacketTailFirst;
        public static byte PacketTailSecond;


        //TODO: verification: head,tail, pkey, keyid, cmd check. Length for cmd check. version check.  
        //
             
        public static EncryptionKey ResetEncryptionKey()
        {
            return _EncryptionKeys.SetCurrentKey();
        }

        public static EncryptionKey FindEncryptionKey(byte keyId)
        {
            return _EncryptionKeys.FindEncryptionKey(keyId);
        }

      
        public static event InitializedDelegate OnInit;

        public static void Init(int userId, byte useerPrivateKey, List<EncryptionKey> keys,
                                byte packetHeadFirst, byte packetHeadSecond, byte packetTailFirst, byte packetTailSecond)
        {
            _EncryptionKeys.ReloadAllKeys(keys);
            SetCurrentUserId(userId);
            _EncryptionKeys.SetCurrentKey();
            PacketManager.CurrentUserPrivateKey = useerPrivateKey;

            PacketManager.PacketHeadFirst = packetHeadFirst; 
            PacketManager.PacketHeadSecond = packetHeadSecond; 
            PacketManager.PacketTailFirst = packetTailFirst;
            PacketManager.PacketTailSecond = packetTailSecond;

            if (OnInit != null)
            {
                OnInit.Invoke();
            }

         }


        public unsafe static void SetCurrentUserId(int userId)
        {
            byte* bytes = ((byte*)&userId);
            CurrentUserIdBytes[0] = bytes[0];
            CurrentUserIdBytes[1] = bytes[1];
            CurrentUserIdBytes[2] = bytes[2];
            CurrentUserIdBytes[3] = bytes[3];
            CurrentUserId = userId;
        }


        //public static int GetBufferSizeForCmd(byte cmdGroup, byte cmd)
        //{
        //    if(cmdGroup== NetworkCommandType.PlayerActionCommands.CommandGroup)
        //    {
        //        if (cmd == NetworkCommandType.PlayerActionCommands.PlayerData_Position) return 44;
        //        else if (cmd == NetworkCommandType.PlayerActionCommands.PlayerData_Aim) return 64;

        //    }
        //    return 64;
        //}



    }
}
