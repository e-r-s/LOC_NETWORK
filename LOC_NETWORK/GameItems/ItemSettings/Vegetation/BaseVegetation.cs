using System;
using LOC_NETWORK.GameItems.Base.Elements;

namespace LOC_NETWORK.GameItems.Base.ItemSettings.Vegetation
{
    public class BaseVegetation : ItemSetting
    {
        public BaseVegetation()
        {

            this.ItemId = 1111;
            this.Name = "BaseVegetation";

            this.IsAlive = true;
            this.CanWalk = false;
            this.CanSwim = false;
            this.CanFly = false;
            this.CanMove = false;
            this.CanEat = false;
            this.CanBurn = true;
            this.CanBreak = true;
            this.CanRecover = true;
            this.Color = "ffc600";
            this.SmellLevel = 15;
            this.Weight = 50;
            this.BuoyantLevel = 80;
            this.MaxRunSpeedInASecond = 0;
            this.MaxFlySpeedInASecond = 0;
            this.MaxSwimSpeedInASecond = 0;

            this.MaxPossibleTemperature = 150;
            this.MinPossibleTemperature = -70;
            this.MaxPossibleHealth = 1500;
            this.MaxPossiblePoisonLevel = 80;
            this.MaxPossibleRecoverTimes = 100;
            this.MaxInteractionRange = 20;
            this.CanSelfInteract = false;
            this.IsInvisible = false;
            this.CanBeGhost = false;
            this.IsGhost = false;

            this.CanRecyle = true;
            this.RecycleRate = 80;

            this.ElementsMakeItStrong = new Element[] {
                    AllBaseElements.Water,
                    AllBaseElements.Oxygen
            };

            this.ElementsMakeItWeak = new Element[] {
                    AllBaseElements.Acid,
                    AllBaseElements.Vinegar
            };

            this.Ingredients = new ElementContent[] {
                   new ElementContent( AllBaseElements.Wood, 95 ),
                   new ElementContent( AllBaseElements.Waste, 5 )
            };



        }
    }
}
