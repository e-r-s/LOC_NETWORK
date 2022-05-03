using System;
namespace LOC_NETWORK.NetworkCommands
{
    public class BaseNetworkCommand
    {
        public byte NeetworkCommandType { get; set; }


        public BaseNetworkCommand()
        {

        }

        public virtual bool IsEqual(byte[] data)
        {
            return data[0] == NeetworkCommandType;
        }

        public virtual void ProcessData(byte[] data)
        {

            //Read Vector3
            //Red Quaternion
            //read other data 
            //verification
            //Update Server Side Data
            //Send Data to other clients
            

        }
    }
}
