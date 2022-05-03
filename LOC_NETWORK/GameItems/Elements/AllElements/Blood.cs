using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Blood:Element
    {
        public Blood()
        {

            this.TypeOfElement = ElementType.None;
            this.Name = "Blood";

            this.BuoyantLevel = -100;
            this.CanBurn = true;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = true;
            this.Color = "ffffff";
            this.SmellLevel = 10;
            this.Weight = 1;

            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Fat, 10 ),
                   new ElementContent( AllBaseElements.Carbohydrate, 10 ),
                   new ElementContent( AllBaseElements.Protein, 5 ),
                   new ElementContent( AllBaseElements.Vitamin, 5 ),
                   new ElementContent( AllBaseElements.Water, 60 ),
                   new ElementContent( AllBaseElements.Iron, 5 ),
                   new ElementContent( AllBaseElements.Waste, 5 )
            };


        }
    }
}
