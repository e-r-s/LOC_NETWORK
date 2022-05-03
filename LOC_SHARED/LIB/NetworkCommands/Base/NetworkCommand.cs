using System;
namespace LOC_SHARED.NetworkCommands.Base
{
    public class NetworkCommand<T>: BaseNetworkCommand
    {
        /// <summary>
        /// On Data Receeived from Remote sender.
        /// </summary>
        public event PacketReceived<T> OnDataReceived;

        /// <summary>
        /// Call events for received data
        /// </summary>
        /// <param name="result">Received data.</param>
        protected void CallOnDataReceived(T result)
        {
            if (OnDataReceived != null)
            {
                OnDataReceived.Invoke(result, ReceiveDataPacket);
            }
        }
         
    }
}
