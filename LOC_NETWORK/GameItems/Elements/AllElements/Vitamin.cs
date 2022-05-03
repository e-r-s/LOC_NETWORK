using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Vitamin:Element
    {
        public Vitamin()
        {
            this.TypeOfElement = ElementType.None;
            this.Name = "Vitamin";

            this.BuoyantLevel = -100;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = true;
            this.CanEat = false;
            this.Color = "ffffff00";
            this.Elements = null;
            this.SmellLevel = 0;
            this.Weight = 1;

            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Waste, 10 ),
                   new ElementContent( AllBaseElements.Acid, 10 ),
                   new ElementContent( AllBaseElements.Oxygen, 25 ),
                   new ElementContent( AllBaseElements.Hydrogen, 30 ),
                   new ElementContent( AllBaseElements.Carbon, 25 )
            };
        }
    }
}
