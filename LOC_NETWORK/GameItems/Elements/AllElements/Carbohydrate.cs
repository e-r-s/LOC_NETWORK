using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Carbohydrate:Element
    {
        public Carbohydrate()
        {
            this.TypeOfElement = ElementType.None;
            this.Name = "Carbohydrate";

            this.BuoyantLevel = -100;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = true;
            this.CanEat = false;
            this.Color = "ffffff00"; 
            this.SmellLevel = 0;
            this.Weight = 1;

            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Carbon, 25 ),
                   new ElementContent( AllBaseElements.Oxygen, 25 ),
                   new ElementContent( AllBaseElements.Hydrogen, 50 ) 
            };
        }
    }
}
