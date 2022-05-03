using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Hydrogen:Element
    {
        public Hydrogen()
        {
            this.TypeOfElement = ElementType.Hydrogen;
            this.BuoyantLevel = -100;
            this.CanBurn = true;
            this.CanEat = true;
            this.Color = "ffffff00";
            this.Elements = null;
            this.Name = "Hydrogen";
            this.SmellLevel = 0;
            this.Weight = 1;
        }
    }
}
