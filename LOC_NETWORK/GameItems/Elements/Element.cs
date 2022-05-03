using System;
namespace LOC_NETWORK.GameItems.Base
{
    public class Element
    {
         
        public ElementContent[] Elements { get; set; }


        public ElementType TypeOfElement { get; set; } 

        public string Name { get; set; }
         

        public string Color { get; set; }
        public int SmellLevel { get; set; }
        public int Weight { get; set; } 
        public int BuoyantLevel { get; set; }  
        public bool CanEat { get; set; }
        public bool CanBurn { get; set; }
        public bool CanCauseBurn { get; set; }
        public bool CanCauseFreeze { get; set; }


    }
}
