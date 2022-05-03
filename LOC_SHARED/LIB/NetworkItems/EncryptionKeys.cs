using System;
namespace LOC_SHARED.NetworkItems
{
    public class EncryptionKey
    {
        public byte[] Key { get; set; }
        public long TimeCreated { get; set; }
        public byte Id { get; set; }
        public bool IsActive { get; set; }
        public EncryptionKey()
        { 
            this.Key = new byte[32];
            new Random().NextBytes(this.Key);
            this.Id = 0;
            this.IsActive = false;
            this.TimeCreated = DateTime.Now.Ticks;
        }
    }
}
