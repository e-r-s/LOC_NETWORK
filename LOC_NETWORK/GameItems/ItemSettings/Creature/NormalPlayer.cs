using System;
using LOC_SHARED.GameItems.TypesAndConstants;

namespace LOC_NETWORK.GameItems.Base.ItemSettings.Creature
{
    public class NormalPlayer:BaseCreature
    {
        public NormalPlayer()
        {

                this.ItemId = ItemIds.Player.NormalPlayer;
                this.Name = "Basic Player";
                this.CanSwim = true;
                this.CanFly = false;
                this.IsGhost = false;
                this.CanBeGhost = true;
                this.MaxRunSpeedInASecond = 6;
                this.MaxFlySpeedInASecond = 8;
                this.MaxSwimSpeedInASecond = 3;

             

        }
}
}
