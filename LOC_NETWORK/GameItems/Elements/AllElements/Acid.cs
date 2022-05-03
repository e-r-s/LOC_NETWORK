using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Acid: Element
    {
        public Acid()
        {
            this.BuoyantLevel = 0;
            this.CanBurn = false;
            this.CanCauseBurn = true;
            this.CanEat = false;
            this.Color = "ffffff00";
            this.Elements = new ElementContent[]{
                        new ElementContent(AllBaseElements.Carbon, 25),
                        new ElementContent(AllBaseElements.Hydrogen, 50),
                        new ElementContent(AllBaseElements.Oxygen, 25)
            };
            this.Name = "Water";
            this.SmellLevel = 50;
            this.Weight = 100;
            this.TypeOfElement = ElementType.None;
        }
    }
}
