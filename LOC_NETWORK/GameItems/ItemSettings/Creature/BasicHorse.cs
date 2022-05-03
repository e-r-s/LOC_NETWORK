using System;
namespace LOC_NETWORK.GameItems.Base.ItemSettings.Creature
{
    public class BasicHorse : BaseCreature
    {
        public BasicHorse()
        {

            this.ItemId = 1111;
            this.Name = "Basic Horse";
            this.CanSwim = false;
            this.CanFly = false;
            this.IsGhost = false;
            this.MaxRunSpeedInASecond = 8;
            this.MaxFlySpeedInASecond = 8;
            this.MaxSwimSpeedInASecond = 8;
           

        }
    }
}
