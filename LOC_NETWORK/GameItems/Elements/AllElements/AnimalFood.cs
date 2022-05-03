using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class AnimalFood:Element
    {
        public AnimalFood()
        {
            this.TypeOfElement = ElementType.None;
            this.Name = "AnimalFood";

            this.BuoyantLevel = -100;
            this.CanBurn = true;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = true;
            this.Color = "ffffff";
            this.SmellLevel = 10;
            this.Weight = 1;

            this.Elements = new ElementContent[]{
                   new ElementContent( AllBaseElements.Fat, 30 ),
                   new ElementContent( AllBaseElements.Carbohydrate, 25 ),
                   new ElementContent( AllBaseElements.Protein, 20 ),
                   new ElementContent( AllBaseElements.Vitamin, 5 ),
                   new ElementContent( AllBaseElements.Water, 10 ),
                   new ElementContent( AllBaseElements.Iron, 5 ),
                   new ElementContent( AllBaseElements.Waste, 5 )
            };
        }
    }
}
