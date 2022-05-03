using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Nitrogen:Element
    {
        public Nitrogen()
        {
            this.TypeOfElement = ElementType.Nitrogen;
            this.Name = "Nitrogen";

            this.BuoyantLevel = -100;
            this.CanBurn = false;
            this.CanCauseBurn = false;
            this.CanCauseFreeze = true;
            this.CanEat = false;
            this.Color = "ffffff00";
            this.Elements = null;
            this.SmellLevel = 0;
            this.Weight = 1;
        }
    }
}
