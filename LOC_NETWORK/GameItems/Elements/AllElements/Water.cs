using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Water:Element
    {
        public Water()
        {
            this.BuoyantLevel = 0;
            this.CanBurn = false;
            this.CanEat = true;
            this.Color = "ffffff00";
            this.Elements = new ElementContent[]{
                        new ElementContent(AllBaseElements.Oxygen, 30),
                        new ElementContent(AllBaseElements.Hydrogen, 60),
                        new ElementContent(AllBaseElements.Waste, 10)
            };
            this.Name = "Water";
            this.SmellLevel = 0;
            this.Weight = 100;
            this.TypeOfElement = ElementType.None;
        }
    }
}
