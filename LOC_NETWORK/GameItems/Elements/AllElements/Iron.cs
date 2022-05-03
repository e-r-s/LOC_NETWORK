using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Iron:Element
    {
        public Iron()
        {
            this.TypeOfElement = ElementType.Iron;
            this.Name = "Iron";

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
