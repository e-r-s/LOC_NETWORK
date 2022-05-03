using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Silver:Element
    {
        public Silver()
        {
            this.TypeOfElement = ElementType.Silver;
            this.Name = "Silver";

            this.BuoyantLevel = 0;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = false;
            this.CanEat = false;
            this.Color = "cccccc";
            this.SmellLevel = 0;
            this.Weight = 200;

            this.Elements = null;
        }
    }
}
