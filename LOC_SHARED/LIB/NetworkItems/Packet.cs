using System;


using UnityEngine; 

using LOC_SHARED.NetworkCommands;


namespace LOC_SHARED.NetworkItems
{
    public class Packet
    {



        //  private int bufferSize = 0;
        public int bufferIndex = 0;
        private bool packetHeadEnabled = false;
        private bool encryptionEnabled = false;

        private byte packetKey = 0;

        //private byte packetHead1 = 0;
        //private byte packetHead2 = 0;
        //private byte packetTail1 = 0;
        //private byte packetTail2 = 0;

        public byte[] buff = null;
        //private byte[] encryptionKey = null;

        private byte lastKeyItemIndex = 0;


        //TODO
        private short[][] latestIncomingCmdVersions = new short[255][];
        private short[][] latestOutgoingCmdVersions = new short[255][];

        private EncryptionKey _EncryptionKeyData;


        private byte[] DeCompile()
        {
            EncryptionKey key = PacketManager.FindEncryptionKey(this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID]);
            return DeCompile(key);
        }

        private void LogBuffer(int startIndex)
        {
            Util.Logger.Log("******************PACKET CONTENT BEGIN******************");
            Util.Logger.Log("Packet Head First", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_HEAD_FIRST].ToString());
            Util.Logger.Log("Packet Head Second", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_HEAD_SECOND].ToString());

            Util.Logger.Log("Buffer Size 1", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE1].ToString());
            Util.Logger.Log("Buffer Size 2", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE2].ToString());

            Util.Logger.Log("Buffer Encryption Key Id", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID].ToString());

            Util.Logger.Log("CMD Group", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_CMD_GROUP].ToString());
            Util.Logger.Log("CMD", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_CMD].ToString());
            Util.Logger.Log("Data Version", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_DATA_VERSION].ToString());

            Util.Logger.Log("Data Count", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_DATA_COUNT].ToString());

            Util.Logger.Log("Need Response", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_NEEED_RESPONSE].ToString());

            Util.Logger.Log("User Private Key", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_USER_PRIVATE_KEY].ToString());


            Util.Logger.Log("User Id 1", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_USERID1].ToString());
            Util.Logger.Log("User Id 2", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_USERID2].ToString());
            Util.Logger.Log("User Id 3", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_USERID3].ToString());
            Util.Logger.Log("User Id 4", this.buff[startIndex+PacketManager.Constants.BUFFER_INDEX_USERID4].ToString());

            Util.Logger.Log("Packet Tail First", this.buff[buff.Length - PacketManager.Constants.BUFFER_INDEX_TAIL_FIRST].ToString());
            Util.Logger.Log("Packet Tail Second", this.buff[buff.Length - PacketManager.Constants.BUFFER_INDEX_TAIL_SECOND].ToString());

             
            Util.Logger.Log("******************PACKET CONTENT END******************");

            Util.Logger.Log("");
        }

        private byte[] DeCompile(EncryptionKey key)
        {

            Util.Logger.Log("BUFFER TO DECOMPILE:");
            Util.Logger.Log(this.buff,0);
            Util.Logger.Log("");
            Util.Logger.Log("ENCRYPTION KEY TO DECOMPILE:");
            Util.Logger.Log(key.Key,0);
            Util.Logger.Log("");


            LogBuffer(0);


            int bufferLengthToDecompile = this.buff.Length - (
                PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_END_RESERVED +
                PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_START_RESERVED 
                );

            int keyCounter = (bufferLengthToDecompile % key.Key.Length) - 1;

            Util.Logger.Log("KEY COUNTER BEFORE DECOMPILE", keyCounter.ToString());

            lastKeyItemIndex = (byte)(key.Key.Length - 1);

            string hashingDetails = "";

            for (int i = this.buff.Length - (PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_END_RESERVED + 1); i >= PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_START_RESERVED; i--)
            {
                if (keyCounter < 0 || keyCounter == 255)
                {
                    keyCounter = lastKeyItemIndex;
                }
                if (this.buff[i] - key.Key[keyCounter] < 0)
                { 
                    hashingDetails += i + ". " + this.buff[i] + " decompiled with " + key.Key[keyCounter] + " = "+((byte)(this.buff[i] - key.Key[keyCounter] + 256)).ToString();
                    this.buff[i] = (byte)(this.buff[i] - key.Key[keyCounter] + 256);
                }
                else
                {
                    hashingDetails += i + ". " + this.buff[i] + " decompiled with " + key.Key[keyCounter] + " = " + ((byte)(this.buff[i] - key.Key[keyCounter])).ToString();
                    this.buff[i] = (byte)(this.buff[i] - key.Key[keyCounter]);
                }
                hashingDetails += Environment.NewLine;

                keyCounter--;
            }
            Util.Logger.Log("HASHING DETAILS", hashingDetails);

            Util.Logger.Log("DECOMPILED BUFFER:");
            Util.Logger.Log(this.buff,0);
            Util.Logger.Log("");

            LogBuffer(0);

            return this.buff;
        }

        private byte[] Compile()
        {

            int keyCounter = 0;
            EncryptionKey key = PacketManager.CurrentEncryptionKey;
            this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID] = key.Id;


            Util.Logger.Log("BUFFER TO COMPILE:");
            Util.Logger.Log(this.buff,0);
            Util.Logger.Log("");
            Util.Logger.Log("E?NCRYPTION KEY TO COMPILE:");
            Util.Logger.Log(key.Key,0);
            Util.Logger.Log("");

            LogBuffer(0);

            string hashingDetails = "";

            for (int i = PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_START_RESERVED; i < this.buff.Length - PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_END_RESERVED; i++)
            {
                if (keyCounter >= key.Key.Length)
                {
                    keyCounter = 0;
                }

                hashingDetails += i + ". " + this.buff[i] + " hashed with " + key.Key[keyCounter] + " = "+ ((byte)(this.buff[i] + key.Key[keyCounter]));

                hashingDetails += Environment.NewLine;

                this.buff[i] += (key.Key[keyCounter]);
                keyCounter++;

            }

            Util.Logger.Log("HASHING DETAILS", hashingDetails);

            Util.Logger.Log("KEY COUNTER AFTER COMPILE", keyCounter.ToString());
            Util.Logger.Log("");

            Util.Logger.Log("COMPILED BUFFER:");
            Util.Logger.Log(this.buff,0); 
            Util.Logger.Log("");
            LogBuffer(0);

            return this.buff;
        }



        //public Packet(int senderUserId, int bufferSize)
        //{
        //    this.bufferSize = bufferSize;
        //    this.buff = new byte[this.bufferSize];

        //    this.buff[0] = packetKey;

        //    SetCurrentUserIdBytes(senderUserId);

        //    this.buff[1] = currentUserIdBytes[0];
        //    this.buff[2] = currentUserIdBytes[1];
        //    this.buff[3] = currentUserIdBytes[2];
        //    this.buff[4] = currentUserIdBytes[3];

        //    //cmd
        //    this.buff[5] = 0;
        //    this.buff[6] = 0;

        //    //cmd version
        //    this.buff[7] = 0;
        //    this.buff[8] = 0;

        //    this.bufferIndex = 9;
        //}

        ////things to add inside packet: userid, packetindex(byte), cmd, used haskey index, private key,  

        //public Packet(int senderUserId, int bufferSize, byte packetHead1, byte packetHead2, byte packetTail1, byte packetTail2)
        //{
        //    this.bufferSize = bufferSize;
        //    this.buff = new byte[bufferSize + 4];
        //    this.buff[0] = packetKey;
        //    this.buff[1] = packetHead1;
        //    this.buff[2] = packetHead2;

        //    SetCurrentUserIdBytes(senderUserId);

        //    this.buff[3] = currentUserIdBytes[0];
        //    this.buff[4] = currentUserIdBytes[1];
        //    this.buff[5] = currentUserIdBytes[2];
        //    this.buff[6] = currentUserIdBytes[3];

        //    //cmd
        //    this.buff[7] = 0;
        //    this.buff[8] = 0;

        //    //cmd version
        //    this.buff[9] = 0;
        //    this.buff[10] = 0;

        //    this.buff[bufferSize - 1] = packetTail1;
        //    this.buff[bufferSize - 2] = packetTail2;
        //    this.bufferIndex = 11;
        //    this.packetHeadEnabled = true;
        //}

        public Packet()
        {


        }

        /// <summary>
        /// Creates a packet with given information
        /// </summary>
        /// <param name="cmd">Single CMD to send</param>
        public Packet(BaseNetworkCommand cmd)
        {
            this.SetPacketContent(cmd.CmdGroup, cmd.Cmd, cmd.RequireResponse, cmd.BufferSize, 1);
        }


        /// <summary>
        /// Creates a packet with given information
        /// </summary>
        /// <param name="cmd">Multi CMD to send</param>
        /// <param name="dataCount">Total SUB-CMD inside this CMD</param>
        public Packet(BaseNetworkCommand cmd, byte dataCount)
        {
            this.SetPacketContent(cmd.CmdGroup, cmd.Cmd, cmd.RequireResponse, cmd.BufferSize, dataCount);
        }



        public void SetPacketContent(byte cmdGroup, byte cmd, bool needResponse, short bufferSize, byte dataCountIntBuffeer)
        {

            //this.bufferSize = bufferSize;
            this.buff = new byte[bufferSize + PacketManager.Constants.BUFFER_INDEX_CMD_DATA_START + PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_END_RESERVED];

            this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_FIRST] = PacketManager.PacketHeadFirst;
            this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_SECOND] = PacketManager.PacketHeadSecond;

            this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE1] = 0;
            this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE2] = 0;

            this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID] = 0;

            this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] = cmdGroup;
            this.buff[PacketManager.Constants.BUFFER_INDEX_CMD] = cmd;
            this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_VERSION] = 0;

            this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT] = 0;

            if (needResponse)
            {
                this.buff[PacketManager.Constants.BUFFER_INDEX_NEEED_RESPONSE] = 2;
            }
            else
            {
                this.buff[PacketManager.Constants.BUFFER_INDEX_NEEED_RESPONSE] = 1;
            }


            this.buff[PacketManager.Constants.BUFFER_INDEX_USER_PRIVATE_KEY] = PacketManager.CurrentUserPrivateKey;

            this.buff[PacketManager.Constants.BUFFER_INDEX_USERID1] = PacketManager.CurrentUserIdBytes[0];
            this.buff[PacketManager.Constants.BUFFER_INDEX_USERID2] = PacketManager.CurrentUserIdBytes[1];
            this.buff[PacketManager.Constants.BUFFER_INDEX_USERID3] = PacketManager.CurrentUserIdBytes[2];
            this.buff[PacketManager.Constants.BUFFER_INDEX_USERID4] = PacketManager.CurrentUserIdBytes[3];


            this.buff[buff.Length - PacketManager.Constants.BUFFER_INDEX_TAIL_FIRST] = PacketManager.PacketTailFirst;
            this.buff[buff.Length - PacketManager.Constants.BUFFER_INDEX_TAIL_SECOND] = PacketManager.PacketTailSecond;

            this.bufferIndex = PacketManager.Constants.BUFFER_INDEX_CMD_DATA_START;
            this.packetHeadEnabled = true;

            SetDataInfo(bufferSize, dataCountIntBuffeer);

        }




        public Packet(byte cmdGroup, byte cmd, bool requireResponse, short bufferSize, byte dataCountIntBuffeer)
        {


            this.SetPacketContent(cmdGroup, cmd, requireResponse, bufferSize, dataCountIntBuffeer);

            //this.bufferSize = bufferSize;
            //this.buff = new byte[bufferSize + 14];

            //this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_FIRST] = PacketManager.PacketHeadFirst;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_SECOND] = PacketManager.PacketHeadSecond;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE1] = 0;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE2] = 0;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID] = 0;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] = cmdGroup;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_CMD] = cmd;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_VERSION] = 0;



            //this.buff[PacketManager.Constants.BUFFER_INDEX_USER_PRIVATE_KEY] = PacketManager.CurrentUserPrivateKey;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID1] = PacketManager.CurrentUserIdBytes[0];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID2] = PacketManager.CurrentUserIdBytes[2];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID3] = PacketManager.CurrentUserIdBytes[3];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID4] = PacketManager.CurrentUserIdBytes[4];


            //this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT] = 0;

            //this.buff[bufferSize - PacketManager.Constants.BUFFER_INDEX_TAIL_FIRST] = PacketManager.PacketTailFirst;
            //this.buff[bufferSize - PacketManager.Constants.BUFFER_INDEX_TAIL_SECOND] = PacketManager.PacketTailSecond;

            //this.bufferIndex = PacketManager.Constants.BUFFER_INDEX_ENCRYPTED_DATA_START;
            //this.packetHeadEnabled = true;

        }


        public void SetCmdVersion(byte cmdDataVersion)
        {
            this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_VERSION] = cmdDataVersion;
            latestOutgoingCmdVersions[this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP]][this.buff[PacketManager.Constants.BUFFER_INDEX_CMD]] = cmdDataVersion;
        }


        public void SetDataCount(byte bufferDataCount)
        {
            this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT] = bufferDataCount;
        }


        public unsafe void SetDataInfo(short bufferSize, byte dataCountInBuffer)
        {
            byte* bytes = ((byte*)&bufferSize);

            this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE1] = bytes[0];
            this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE2] = bytes[1];
            this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT] = dataCountInBuffer;

        }



        public unsafe short GetBufferSize()
        {
            return BitConverter.ToInt16(buff, PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE1);
        }

        public unsafe byte GetBufferDataCount()
        {
            return this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT];
        }

        //private unsafe void WriteCmdVersionBytes(byte cmdGroup, byte cmd, byte version)
        //{  
        //    this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_VERSION] = version; 

        //    latestOutgoingCmdVersions[cmdGroup][cmd] = version;
        //}


        //public void Reset()
        //{
        //    this.Reset(0, 0, 0);
        //}

        public void Reset(byte cmdGroup, byte cmd, bool requireResponse, byte cmdVersion, short bufferSize, byte dataCountInBuffer)
        {

            this.buff = new byte[bufferSize  + PacketManager.Constants.BUFFER_INDEX_CMD_DATA_START + PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_END_RESERVED];

            this.SetPacketContent(cmdGroup, cmd, requireResponse, bufferSize, dataCountInBuffer);


            //this.buff[PacketManager.Constants.BUFFER_INDEX_USER_PRIVATE_KEY] = PacketManager.CurrentUserPrivateKey;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_FIRST] = PacketManager.PacketHeadFirst;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_HEAD_SECOND] = PacketManager.PacketHeadSecond;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID1] = PacketManager.CurrentUserIdBytes[0];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID2] = PacketManager.CurrentUserIdBytes[2];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID3] = PacketManager.CurrentUserIdBytes[3];
            //this.buff[PacketManager.Constants.BUFFER_INDEX_USERID4] = PacketManager.CurrentUserIdBytes[4];

            //this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP] = cmdGroup;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_CMD] = cmd;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_VERSION] = cmdVersion;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID] = 0;

            //this.buff[PacketManager.Constants.BUFFER_INDEX_BUFFER_SIZE] = 0;
            //this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_COUNT] = 0;

            //this.buff[bufferSize - PacketManager.Constants.BUFFER_INDEX_TAIL_FIRST] = PacketManager.PacketTailFirst;
            //this.buff[bufferSize - PacketManager.Constants.BUFFER_INDEX_TAIL_SECOND] = PacketManager.PacketTailSecond;

            //this.bufferIndex = PacketManager.Constants.BUFFER_INDEX_ENCRYPTED_DATA_START;


            if (cmdGroup > 0)
            {
                SetCmdVersion(cmdVersion);
            }
        }




        public int GetPacketOwner()
        {
            return BitConverter.ToInt32(buff, PacketManager.Constants.BUFFER_INDEX_USERID1);
        }

        public byte GetDataCmdGroup()
        {
            return this.buff[PacketManager.Constants.BUFFER_INDEX_CMD_GROUP];
        }

        public byte GetDataCmd()
        {
            return this.buff[PacketManager.Constants.BUFFER_INDEX_CMD];
        }

        public byte GetDataVersion()
        {
            return this.buff[PacketManager.Constants.BUFFER_INDEX_DATA_VERSION];
        }

        public byte GetEncryptionKeyId()
        {
            return this.buff[PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID];
        }




        /// <summary>
        /// Enables Auto Encryption on Buffer. Encryption key is not stored. 
        /// </summary>
        public void EnableEncryption()
        {
            this.encryptionEnabled = true;
            // this.encryptionKey = new byte[0];
        }

        /// <summary>
        /// Disables Auto Encryption on Buffer. 
        /// </summary>
        public void DisableEncryption()
        {
            this.encryptionEnabled = false;
        }



        /// <summary>
        /// Sets sent byte[] data to the Buffer. This method also does simple verification on buffer. If Encryption is enabled, buffer will be decompiled with given key.
        /// </summary>
        /// <param name="buffer">data to load</param>
        /// <param name="key">Encryption key</param>
        /// <returns>Whether buffer is successfully verified and loaded</returns>
        public bool LoadBuffer(byte[] buffer, int startIndex, int length)
        {
            //Reset current buffer
            //this.Reset();

            //int bufferSizeForCmd = PacketManager.GetBufferSizeForCmd(

            //             );

            if (
                //buffer[startIndex+PacketManager.Constants.BUFFER_INDEX_USER_PRIVATE_KEY] == PacketManager.CurrentUserPrivateKey &&
                buffer[startIndex+PacketManager.Constants.BUFFER_INDEX_HEAD_FIRST] == PacketManager.PacketHeadFirst &&
                buffer[startIndex+PacketManager.Constants.BUFFER_INDEX_HEAD_SECOND] == PacketManager.PacketHeadSecond &&
                buffer[startIndex + length - PacketManager.Constants.BUFFER_INDEX_TAIL_FIRST] == PacketManager.PacketTailFirst &&
                buffer[startIndex + length - PacketManager.Constants.BUFFER_INDEX_TAIL_SECOND] == PacketManager.PacketTailSecond
            )
            {
                EncryptionKey key = PacketManager.FindEncryptionKey(buffer[startIndex+PacketManager.Constants.BUFFER_INDEX_ENCRYPTION_KEY_ID]);
                if (key != null)
                {
                    if (startIndex > 0)
                    {
                        this.buff = new byte[length];
                        int indexer = 0;
                           for (int i = startIndex; indexer < length; i++)
                            {
                                this.buff[indexer] = buffer[i];
                                indexer++;
                            }
                    }
                    else
                    {
                        this.buff = buffer;
                    }
                    this.DeCompile(key);
                    this.bufferIndex = PacketManager.Constants.BUFFER_INDEX_CMD_DATA_START;
                    return true;
                }

            }
            // AllNetworkCommands.PlayerActionCommands.PlayerData_Position.
            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public void LoadBufferRaw(byte[] buffer)
        {
            this.buff = buffer;
            this.bufferIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public void LoadRawData(byte[] data, int startIndex, int endIndex, int destStartIndex)
        {
            int indexer = destStartIndex;
            for (var i = startIndex; i < endIndex; i++)
            {
                this.buff[indexer] = data[i];
                indexer++;
            }
            this.bufferIndex = 0;
        }


        public byte[] GetBufferRaw()
        {
            return this.buff;
        }

        public byte[] GetBufferEncrypted()
        {
            return Compile();
        }

        public byte[] GetBufferDecrypted()
        {
            return DeCompile();
        }


        public byte[] GetBufferDataRaw(int length)
        {

            byte[] destinationArray = new byte[length];
            System.Buffer.BlockCopy(buff, PacketManager.Constants.BUFFER_INDEX_CMD_DATA_START, destinationArray, 0, length);
            return destinationArray;
        }


        public bool ReadBoolean()
        {
            bufferIndex += 1;
            return buff[bufferIndex - 1] == 2;
        }

        public byte ReadByte()
        {
            bufferIndex += 1;
            return buff[bufferIndex - 1];
        }


        public byte[] ReadBytes(int length)
        {
            bufferIndex += length;
            byte[] destinationArray = new byte[length];
            System.Buffer.BlockCopy(buff, bufferIndex - length, destinationArray, 0, length);
            return destinationArray;
        }

        public short ReadShort()
        {
            bufferIndex += 2;
            return BitConverter.ToInt16(buff, bufferIndex - 2);
        }
        public int ReadInt()
        {
            bufferIndex += 4;
            return BitConverter.ToInt32(buff, bufferIndex - 4);
        }
        public float ReadFloat()
        {
            bufferIndex += 4;
            return BitConverter.ToSingle(buff, bufferIndex - 4);
        }
        public double ReadDouble()
        {
            bufferIndex += 8;
            return BitConverter.ToDouble(buff, bufferIndex - 8);
        }
        public long ReadLong()
        {
            bufferIndex += 8;
            return BitConverter.ToInt64(buff, bufferIndex - 8);
        }
        public Vector3 ReadVector3()
        {
            bufferIndex += 12;
            Vector3 res =  new Vector3(BitConverter.ToSingle(buff, bufferIndex - 12), BitConverter.ToSingle(buff, bufferIndex - 8), BitConverter.ToSingle(buff, bufferIndex - 4));
            Util.Logger.Log("REEADING VECTOR 3 FROM :");
            Util.Logger.Log(buff, bufferIndex - 12); 
            Util.Logger.Log("VECTOR3 READ RESULT :      x:" + res.x + ",y:" + res.y + ",z:" + res.z + "---");
            Util.Logger.Log("");
            return res;
        }
        public void ReadVector3(ref Vector3 vectorReference)
        {

            bufferIndex += 12;

//#if UNITY_EDITOR
            vectorReference.x = BitConverter.ToSingle(buff, bufferIndex - 12);
            vectorReference.y = BitConverter.ToSingle(buff, bufferIndex - 8);
            vectorReference.z = BitConverter.ToSingle(buff, bufferIndex - 4);
//#else
//            vectorReference.X = BitConverter.ToSingle(buff, bufferIndex - 12);
//            vectorReference.Y = BitConverter.ToSingle(buff, bufferIndex - 8);
//            vectorReference.Z = BitConverter.ToSingle(buff, bufferIndex - 4);
//#endif


        }

        public Quaternion ReadQuaternion()
        {
            bufferIndex += 16;
            return new Quaternion(BitConverter.ToSingle(buff, bufferIndex - 16), BitConverter.ToSingle(buff, bufferIndex - 12), BitConverter.ToSingle(buff, bufferIndex - 8), BitConverter.ToSingle(buff, bufferIndex - 4));
        }
        public void ReadQuaternion(ref Quaternion quaternionReference)
        {
            bufferIndex += 16;

//#if UNITY_EDITOR 
            quaternionReference.x = BitConverter.ToSingle(buff, bufferIndex - 16);
            quaternionReference.y = BitConverter.ToSingle(buff, bufferIndex - 12);
            quaternionReference.z = BitConverter.ToSingle(buff, bufferIndex - 8);
            quaternionReference.w = BitConverter.ToSingle(buff, bufferIndex - 4);
//#else 
//            quaternionReference.X = BitConverter.ToSingle(buff, bufferIndex - 16);
//            quaternionReference.Y = BitConverter.ToSingle(buff, bufferIndex - 12);
//            quaternionReference.Z = BitConverter.ToSingle(buff, bufferIndex - 8);
//            quaternionReference.W = BitConverter.ToSingle(buff, bufferIndex - 4);
//#endif

        }



        //private unsafe void SetCurrentUserIdBytes(int userId)
        //{
        //    byte* bytes = ((byte*)&userId);
        //    currentUserIdBytes[0] = bytes[0];
        //    currentUserIdBytes[1] = bytes[1];
        //    currentUserIdBytes[2] = bytes[2];
        //    currentUserIdBytes[3] = bytes[3];
        //}


        public unsafe void WriteBytes(bool variable)
        {
            bufferIndex += 1;
            buff[bufferIndex - 1] = (byte)(variable ? 2 : 1);
        }

        public unsafe void WriteBytes(byte variable)
        {
            bufferIndex += 1;
            buff[bufferIndex - 1] = variable;
        }


        public unsafe void WriteBytes(short variable)
        {
            bufferIndex += 2;
            byte* bytes = ((byte*)&variable);
            buff[bufferIndex - 2] = bytes[0];
            buff[bufferIndex - 1] = bytes[1];
        }

        public unsafe void WriteBytes(int variable)
        {
            bufferIndex += 4;

            byte* bytes = ((byte*)&variable);
            buff[bufferIndex - 4] = bytes[0];
            buff[bufferIndex - 3] = bytes[1];
            buff[bufferIndex - 2] = bytes[2];
            buff[bufferIndex - 1] = bytes[3];
        }

        public unsafe void WriteBytes(float variable)
        {

            bufferIndex += 4;

            byte* bytes = ((byte*)&variable);
            buff[bufferIndex - 4] = bytes[0];
            buff[bufferIndex - 3] = bytes[1];
            buff[bufferIndex - 2] = bytes[2];
            buff[bufferIndex - 1] = bytes[3];

        }

        public unsafe void WriteBytes(long variable)
        {

            bufferIndex += 8;

            byte* bytes = ((byte*)&variable);
            buff[bufferIndex - 8] = bytes[0];
            buff[bufferIndex - 7] = bytes[1];
            buff[bufferIndex - 6] = bytes[2];
            buff[bufferIndex - 5] = bytes[3];
            buff[bufferIndex - 4] = bytes[4];
            buff[bufferIndex - 3] = bytes[5];
            buff[bufferIndex - 2] = bytes[6];
            buff[bufferIndex - 1] = bytes[7];

        }

        public unsafe void WriteBytes(double variable)
        {

            bufferIndex += 8;

            byte* bytes = ((byte*)&variable);
            buff[bufferIndex - 8] = bytes[0];
            buff[bufferIndex - 7] = bytes[1];
            buff[bufferIndex - 6] = bytes[2];
            buff[bufferIndex - 5] = bytes[3];
            buff[bufferIndex - 4] = bytes[4];
            buff[bufferIndex - 3] = bytes[5];
            buff[bufferIndex - 2] = bytes[6];
            buff[bufferIndex - 1] = bytes[7];

        }



        public unsafe void WriteBytes(Vector3 variable)
        {

            bufferIndex += 12;


//#if UNITY_EDITOR 
            byte* bytes = ((byte*)&variable.x);
            buff[bufferIndex - 12] = bytes[0];
            buff[bufferIndex - 11] = bytes[1];
            buff[bufferIndex - 10] = bytes[2];
            buff[bufferIndex - 9] = bytes[3];


            bytes = ((byte*)&variable.y);
            buff[bufferIndex - 8] = bytes[0];
            buff[bufferIndex - 7] = bytes[1];
            buff[bufferIndex - 6] = bytes[2];
            buff[bufferIndex - 5] = bytes[3];


            bytes = ((byte*)&variable.z);
            buff[bufferIndex - 4] = bytes[0];
            buff[bufferIndex - 3] = bytes[1];
            buff[bufferIndex - 2] = bytes[2];
            buff[bufferIndex - 1] = bytes[3];

            Util.Logger.Log("x:"+variable.x+",y:"+variable.y+",z:"+variable.z+" IN VECTOR 3 CONVERTED AS :");
            Util.Logger.Log(buff, bufferIndex - 12); 
            Util.Logger.Log("");
//#else        
//            byte* bytes = ((byte*)&variable.X);
//            buff[bufferIndex - 12] = bytes[0];
//            buff[bufferIndex - 11] = bytes[1];
//            buff[bufferIndex - 10] = bytes[2];
//            buff[bufferIndex - 9] = bytes[3];


//            bytes = ((byte*)&variable.Y);
//            buff[bufferIndex - 8] = bytes[4];
//            buff[bufferIndex - 7] = bytes[5];
//            buff[bufferIndex - 6] = bytes[6];
//            buff[bufferIndex - 5] = bytes[7];


//            bytes = ((byte*)&variable.Z);
//            buff[bufferIndex - 4] = bytes[8];
//            buff[bufferIndex - 3] = bytes[9];
//            buff[bufferIndex - 2] = bytes[10];
//            buff[bufferIndex - 1] = bytes[11];
//#endif




        }


        public unsafe void WriteBytes(Quaternion variable)
        {

            bufferIndex += 16;

//#if UNITY_EDITOR 
            byte* bytes = ((byte*)&variable.x);
            buff[bufferIndex - 16] = bytes[0];
            buff[bufferIndex - 15] = bytes[1];
            buff[bufferIndex - 14] = bytes[2];
            buff[bufferIndex - 13] = bytes[3];


            bytes = ((byte*)&variable.y);
            buff[bufferIndex - 12] = bytes[0];
            buff[bufferIndex - 11] = bytes[1];
            buff[bufferIndex - 10] = bytes[2];
            buff[bufferIndex - 9] = bytes[3];


            bytes = ((byte*)&variable.z);
            buff[bufferIndex - 8] = bytes[0];
            buff[bufferIndex - 7] = bytes[1];
            buff[bufferIndex - 6] = bytes[2];
            buff[bufferIndex - 5] = bytes[3];



            bytes = ((byte*)&variable.w);
            buff[bufferIndex - 4] = bytes[0];
            buff[bufferIndex - 3] = bytes[2];
            buff[bufferIndex - 2] = bytes[3];
            buff[bufferIndex - 1] = bytes[4];

//#else  
//            byte* bytes = ((byte*)&variable.X);
//            buff[bufferIndex - 16] = bytes[0];
//            buff[bufferIndex - 15] = bytes[1];
//            buff[bufferIndex - 14] = bytes[2];
//            buff[bufferIndex - 13] = bytes[3];


//            bytes = ((byte*)&variable.Y);
//            buff[bufferIndex - 12] = bytes[4];
//            buff[bufferIndex - 11] = bytes[5];
//            buff[bufferIndex - 10] = bytes[6];
//            buff[bufferIndex - 9] = bytes[7];


//            bytes = ((byte*)&variable.Z);
//            buff[bufferIndex - 8] = bytes[8];
//            buff[bufferIndex - 7] = bytes[9];
//            buff[bufferIndex - 6] = bytes[10];
//            buff[bufferIndex - 5] = bytes[11];



//            bytes = ((byte*)&variable.W);
//            buff[bufferIndex - 4] = bytes[12];
//            buff[bufferIndex - 3] = bytes[13];
//            buff[bufferIndex - 2] = bytes[14];
//            buff[bufferIndex - 1] = bytes[15];

//#endif

        }





























        public static bool ReadBoolean(byte[] buff, int bufferIndex)
        {
            return buff[bufferIndex] == 2;
        }

        public static byte ReadByte(byte[] buff, int bufferIndex)
        {
            return buff[bufferIndex];
        }


        public static byte[] ReadBytes(byte[] buff, int bufferIndex, int length)
        {
            byte[] destinationArray = new byte[length];
            System.Buffer.BlockCopy(buff, bufferIndex, destinationArray, 0, length);
            return destinationArray;
        }

        public static short ReadShort(byte[] buff, int bufferIndex)
        {
            return BitConverter.ToInt16(buff, bufferIndex);
        }
        public static int ReadInt(byte[] buff, int bufferIndex)
        {
            return BitConverter.ToInt32(buff, bufferIndex);
        }
        public static float ReadFloat(byte[] buff, int bufferIndex)
        {
            return BitConverter.ToSingle(buff, bufferIndex);
        }
        public static double ReadDouble(byte[] buff, int bufferIndex)
        {
            return BitConverter.ToDouble(buff, bufferIndex);
        }
        public static long ReadLong(byte[] buff, int bufferIndex)
        {
            return BitConverter.ToInt64(buff, bufferIndex);
        }
        public static Vector3 ReadVector3(byte[] buff, int bufferIndex)
        {
            return new Vector3(BitConverter.ToSingle(buff, bufferIndex), BitConverter.ToSingle(buff, bufferIndex + 4), BitConverter.ToSingle(buff, bufferIndex + 8));
        }
        public static void ReadVector3(byte[] buff, int bufferIndex, ref Vector3 vectorReference)
        {

//#if UNITY_EDITOR
            vectorReference.x = BitConverter.ToSingle(buff, bufferIndex);
            vectorReference.y = BitConverter.ToSingle(buff, bufferIndex + 4);
            vectorReference.z = BitConverter.ToSingle(buff, bufferIndex + 8);
//#else
//            vectorReference.X = BitConverter.ToSingle(buff, bufferIndex);
//            vectorReference.Y = BitConverter.ToSingle(buff, bufferIndex + 4);
//            vectorReference.Z = BitConverter.ToSingle(buff, bufferIndex + 8);
//#endif


        }

        public static Quaternion ReadQuaternion(byte[] buff, int bufferIndex)
        {
            return new Quaternion(BitConverter.ToSingle(buff, bufferIndex), BitConverter.ToSingle(buff, bufferIndex + 4), BitConverter.ToSingle(buff, bufferIndex + 8), BitConverter.ToSingle(buff, bufferIndex + 12));
        }
        public static void ReadQuaternion(byte[] buff, int bufferIndex, ref Quaternion quaternionReference)
        {
//#if UNITY_EDITOR
            quaternionReference.x = BitConverter.ToSingle(buff, bufferIndex);
            quaternionReference.y = BitConverter.ToSingle(buff, bufferIndex + 4);
            quaternionReference.z = BitConverter.ToSingle(buff, bufferIndex + 8);
            quaternionReference.w = BitConverter.ToSingle(buff, bufferIndex + 12);
//#else
//            quaternionReference.X = BitConverter.ToSingle(buff, bufferIndex);
//            quaternionReference.Y = BitConverter.ToSingle(buff, bufferIndex + 4);
//            quaternionReference.Z = BitConverter.ToSingle(buff, bufferIndex + 8);
//            quaternionReference.W = BitConverter.ToSingle(buff, bufferIndex + 12);
//#endif


        }





        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, bool variable)
        {
            buff[bufferIndex] = (byte)(variable ? 2 : 1);
        }

        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, byte variable)
        {
            buff[bufferIndex] = variable;
        }


        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, byte[] variable)
        {
            for (int i = 0; i < variable.Length; i++)
            {
                buff[bufferIndex + i] = variable[i];
            }
        }


        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, short variable)
        {
            byte* bytes = ((byte*)&variable);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
        }

        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, int variable)
        {
            byte* bytes = ((byte*)&variable);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];
        }

        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, float variable)
        {
            byte* bytes = ((byte*)&variable);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];

        }

        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, long variable)
        {
            byte* bytes = ((byte*)&variable);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];
            buff[bufferIndex + 4] = bytes[4];
            buff[bufferIndex + 5] = bytes[5];
            buff[bufferIndex + 6] = bytes[6];
            buff[bufferIndex + 7] = bytes[7];

        }

        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, double variable)
        {

            byte* bytes = ((byte*)&variable);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];
            buff[bufferIndex + 4] = bytes[4];
            buff[bufferIndex + 5] = bytes[5];
            buff[bufferIndex + 6] = bytes[6];
            buff[bufferIndex + 7] = bytes[7];

        }



        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, Vector3 variable)
        {

//#if UNITY_EDITOR
            byte* bytes = ((byte*)&variable.x);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];


            bytes = ((byte*)&variable.y);
            buff[bufferIndex + 4] = bytes[4];
            buff[bufferIndex + 5] = bytes[5];
            buff[bufferIndex + 6] = bytes[6];
            buff[bufferIndex + 7] = bytes[7];


            bytes = ((byte*)&variable.z);
            buff[bufferIndex + 8] = bytes[8];
            buff[bufferIndex + 9] = bytes[9];
            buff[bufferIndex + 10] = bytes[10];
            buff[bufferIndex + 11] = bytes[11];
//#else
//            byte* bytes = ((byte*)&variable.X);
//            buff[bufferIndex] = bytes[0];
//            buff[bufferIndex + 1] = bytes[1];
//            buff[bufferIndex + 2] = bytes[2];
//            buff[bufferIndex + 3] = bytes[3];


//            bytes = ((byte*)&variable.Y);
//            buff[bufferIndex + 4] = bytes[4];
//            buff[bufferIndex + 5] = bytes[5];
//            buff[bufferIndex + 6] = bytes[6];
//            buff[bufferIndex + 7] = bytes[7];


//            bytes = ((byte*)&variable.Z);
//            buff[bufferIndex + 8] = bytes[8];
//            buff[bufferIndex + 9] = bytes[9];
//            buff[bufferIndex + 10] = bytes[10];
//            buff[bufferIndex + 11] = bytes[11];
//#endif
        }


        public static unsafe void WriteBytes(byte[] buff, int bufferIndex, Quaternion variable)
        {

//#if UNITY_EDITOR
            byte* bytes = ((byte*)&variable.x);
            buff[bufferIndex] = bytes[0];
            buff[bufferIndex + 1] = bytes[1];
            buff[bufferIndex + 2] = bytes[2];
            buff[bufferIndex + 3] = bytes[3];
             
            bytes = ((byte*)&variable.y);
            buff[bufferIndex + 4] = bytes[4];
            buff[bufferIndex + 5] = bytes[5];
            buff[bufferIndex + 6] = bytes[6];
            buff[bufferIndex + 7] = bytes[7];
             
            bytes = ((byte*)&variable.z);
            buff[bufferIndex + 8] = bytes[8];
            buff[bufferIndex + 9] = bytes[9];
            buff[bufferIndex + 10] = bytes[10];
            buff[bufferIndex + 11] = bytes[11];
             
            bytes = ((byte*)&variable.w);
            buff[bufferIndex + 12] = bytes[12];
            buff[bufferIndex + 13] = bytes[13];
            buff[bufferIndex + 14] = bytes[14];
            buff[bufferIndex + 15] = bytes[15];
//#else
//            byte* bytes = ((byte*)&variable.X);
//            buff[bufferIndex] = bytes[0];
//            buff[bufferIndex + 1] = bytes[1];
//            buff[bufferIndex + 2] = bytes[2];
//            buff[bufferIndex + 3] = bytes[3];

//            bytes = ((byte*)&variable.Y);
//            buff[bufferIndex + 4] = bytes[4];
//            buff[bufferIndex + 5] = bytes[5];
//            buff[bufferIndex + 6] = bytes[6];
//            buff[bufferIndex + 7] = bytes[7];

//            bytes = ((byte*)&variable.Z);
//            buff[bufferIndex + 8] = bytes[8];
//            buff[bufferIndex + 9] = bytes[9];
//            buff[bufferIndex + 10] = bytes[10];
//            buff[bufferIndex + 11] = bytes[11];

//            bytes = ((byte*)&variable.W);
//            buff[bufferIndex + 12] = bytes[12];
//            buff[bufferIndex + 13] = bytes[13];
//            buff[bufferIndex + 14] = bytes[14];
//            buff[bufferIndex + 15] = bytes[15];
//#endif

        }


    }
}
