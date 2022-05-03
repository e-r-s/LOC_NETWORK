using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Vinegar:Element
    {
        public Vinegar()
        {
            this.BuoyantLevel = 0;
            this.CanBurn = false;
            this.CanEat = true;
            this.Color = "ffffff00";
            this.Elements = new ElementContent[]{
                        new ElementContent(AllBaseElements.Acid, 20),
                        new ElementContent(AllBaseElements.Water, 80)
            };
            this.Name = "Vinegar";
            this.SmellLevel = 50;
            this.Weight = 100;
            this.TypeOfElement = ElementType.None;

        }
    }
}
