using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Protein : Element
    {
        public Protein()
        {
            this.TypeOfElement = ElementType.None;
            this.Name = "Protein";

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
                   new ElementContent( AllBaseElements.Carbon, 20 ),
                   new ElementContent( AllBaseElements.Oxygen, 20 ),
                   new ElementContent( AllBaseElements.Hydrogen, 40 ),
                   new ElementContent( AllBaseElements.Nitrogen, 20 )
            };

        }
    }
}
