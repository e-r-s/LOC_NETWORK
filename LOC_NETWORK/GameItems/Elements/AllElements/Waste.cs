using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Waste:Element
    {
        public Waste()
        {
            this.TypeOfElement = ElementType.None;
            this.Name = "Waste";

            this.BuoyantLevel = 1;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = false;
            this.Color = "aaaaaa";
            this.Elements = null;
            this.SmellLevel = 10;
            this.Weight = 150;
        }
    }
}
