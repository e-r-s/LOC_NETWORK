using System;
namespace LOC_SHARED.NetworkCommands.Base
{
    /// <summary>
    /// Raw command data used for multiple data packet.
    /// </summary>
    public class RawCommandData
    {
        /// <summary>
        /// Data owner
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Data cmd group
        /// </summary>
        public byte CmdGroup { get; set; }
        /// <summary>
        /// Data cmd
        /// </summary>
        public byte Cmd { get; set; }
        /// <summary>
        /// Cmd instance used by this data
        /// </summary>
        public BaseNetworkCommand Command { get; set; }
        /// <summary>
        /// data as byte array not including user id.
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Created time. RawCommandData can be used to send data as parameter. In such case this parameter has value.  
        /// </summary>
        public long CreatedTime { get; set; }
    }
}
