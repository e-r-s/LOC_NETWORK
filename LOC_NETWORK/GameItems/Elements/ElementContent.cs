using System;
namespace LOC_NETWORK.GameItems.Base
{
    public class ElementContent
    {
        public Element ElementInside { get; set; }
        public int Rate { get; set; }
        public ElementContent()
        {

        }
        public ElementContent(Element elementInside, int rate)
        {
            this.ElementInside = elementInside;
            this.Rate = rate;
        }
    }
}
