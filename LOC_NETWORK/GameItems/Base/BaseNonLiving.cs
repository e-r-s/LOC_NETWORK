using System;
namespace LOC_NETWORK.GameItems.Base
{
    public class BaseNonLiving
    {
        public bool IsBurning { get; set; }
        public bool IsDamaged { get; set; }

        public int BurnedTimeLeft { get; set; }
        public int DamagedTimeLeft { get; set; }

        public int ApplyRecoverTimeLeft { get; set; }
        public int RecoveredTimes { get; set; }

        public int TemperatureLevel { get; set; }
        public int PoisonLevel { get; set; }
        public int SickLevel { get; set; }


    }
}
