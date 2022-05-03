using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Oxygen:Element
    {
        public Oxygen()
        {
            this.TypeOfElement = ElementType.Oxygen;
            this.BuoyantLevel = -100;
            this.CanBurn = false;
            this.CanEat = true;
            this.Color = "ffffff00";
            this.Elements = null;
            this.Name = "Oxygen";
            this.SmellLevel = 0;
            this.Weight = 1; 

        }
    }
}
