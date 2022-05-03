using System;
namespace LOC_NETWORK.GameItems.Base
{
    public class BaseLiving:BaseItem
    {
        public bool IsPoisoned { get; set; }
        public bool IsBurning { get; set; }
        public bool IsSick { get; set; }
        public bool IsFreezing { get; set; }
        public bool IsWounded { get; set; }

        public int PoisonedTimeLeft { get; set; }
        public int BurnedTimeLeft { get; set; }
        public int SickTimeLeft { get; set; }
        public int FrozenTimeLeft { get; set; }
        public int WoundedTimeLeft { get; set; }

        public int ApplyRecoverTimeLeft { get; set; }
        public int RecoveredTimes { get; set; }

        public int TemperatureLevel { get; set; }
        public int PoisonLevel { get; set; }
        public int SickLevel { get; set; }


    }
}
