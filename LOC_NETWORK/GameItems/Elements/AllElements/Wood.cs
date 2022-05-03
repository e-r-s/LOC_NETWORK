using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Wood:Element
    {
        public Wood()
        {
            this.BuoyantLevel = 0;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = true;
            this.Color = "ffc600";
            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Carbon, 45 ),
                   new ElementContent( AllBaseElements.Oxygen, 30 ),
                   new ElementContent( AllBaseElements.Hydrogen, 3 ),
                   new ElementContent( AllBaseElements.Nitrogen, 1 ),
                   new ElementContent( AllBaseElements.Water, 10 ),
                   new ElementContent( AllBaseElements.Waste, 11 )
            };
            this.Name = "Wood";
            this.SmellLevel = 0;
            this.Weight = 100;
            this.TypeOfElement = ElementType.None;
        }
    }
}
