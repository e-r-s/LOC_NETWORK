using System;
using LOC_NETWORK.GameItems.Base;

namespace LOC_NETWORK.GameItems.Vegetation
{
    public class BaseVegetation: BaseLiving
    {
        public BaseVegetation()
        {
            this.Location = new ItemLocation();
        }
    }
}
