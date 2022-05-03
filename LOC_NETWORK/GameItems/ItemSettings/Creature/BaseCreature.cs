using System;
using LOC_NETWORK.GameItems.Base.Elements;

namespace LOC_NETWORK.GameItems.Base.ItemSettings.Creature
{
    public class BaseCreature:ItemSetting
    {
        public BaseCreature()
        {

            this.ItemId = 1111;
            this.Name = "BaseCreature";
            this.CanSwim = false;
            this.CanFly = false;
            this.IsGhost = false;


            this.IsAlive = true;
            this.CanWalk = true;
            this.CanMove = true;
            this.CanEat = true;
            this.CanBurn = true;
            this.CanBreak = true;
            this.CanRecover = true;
            this.Color = "ffffff";
            this.SmellLevel = 10;
            this.Weight = 99;
            this.BuoyantLevel = 10;
            this.MaxRunSpeedInASecond = 6;
            this.MaxFlySpeedInASecond = 8;
            this.MaxSwimSpeedInASecond = 3;

            this.MaxPossibleTemperature = 100;
            this.MinPossibleTemperature = -50;
            this.MaxPossibleHealth = 200;
            this.MaxPossiblePoisonLevel = 80;
            this.MaxPossibleRecoverTimes = 100;
            this.MaxInteractionRange = 50;
            this.CanSelfInteract = true;
            this.IsInvisible = false;
            this.CanBeGhost = true;

            this.CanRecyle = true;
            this.RecycleRate = 50;

            this.ElementsMakeItStrong = new Element[] {
                    AllBaseElements.Water,
                    AllBaseElements.Oxygen,
                    AllBaseElements.AnimalFood,
                    AllBaseElements.VegetationFood,
                    AllBaseElements.Fat,
                    AllBaseElements.Protein,
                    AllBaseElements.Vitamin
            };

            this.ElementsMakeItWeak = new Element[] {
                    AllBaseElements.Acid,
                    AllBaseElements.Vinegar,
                    AllBaseElements.Silver
            };

            this.Ingredients = new ElementContent[] {
                   new ElementContent( AllBaseElements.AnimalFood, 40 ),
                   new ElementContent( AllBaseElements.Blood, 50 ), 
                   new ElementContent( AllBaseElements.Fat, 5 ), 
                   new ElementContent( AllBaseElements.Waste, 5 )
            };


        }
    }
}
