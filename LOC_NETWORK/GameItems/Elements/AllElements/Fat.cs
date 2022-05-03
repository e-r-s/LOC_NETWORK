using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Fat:Element
    {
        public Fat()
        {
            this.TypeOfElement = ElementType.Nitrogen;
            this.Name = "Fat";

            this.BuoyantLevel = -100;
            this.CanBurn = true;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = true;
            this.Color = "ffffff"; 
            this.SmellLevel = 0;
            this.Weight = 1;

            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Carbon, 25 ),
                   new ElementContent( AllBaseElements.Hydrogen, 40 ),
                   new ElementContent( AllBaseElements.Acid, 20 ),
                   new ElementContent( AllBaseElements.Water, 10 ),
                   new ElementContent( AllBaseElements.Waste, 5 )
            };

        }
    }
}
