using System;
namespace LOC_NETWORK.GameItems.Base.Elements
{
    public class Carbon:Element
    {
        public Carbon()
        {
            this.BuoyantLevel = 70;
            this.CanBurn = true;
            this.CanCauseBurn = false;
            this.CanEat = false;
            this.Color = "000000";
            this.Elements = null;
            this.Name = "Carbon";
            this.SmellLevel = 5;
            this.Weight = 50;
            this.TypeOfElement = ElementType.Carbon;
        }
    }
}
