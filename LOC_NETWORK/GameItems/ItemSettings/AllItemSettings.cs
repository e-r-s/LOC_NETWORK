using System;
using LOC_NETWORK.GameItems.Base.ItemSettings.Creature;
using LOC_NETWORK.GameItems.Base.ItemSettings.Vegetation;

namespace LOC_NETWORK.GameItems.Base.ItemSettings
{
    public static class AllItemSettings
    {
        public static class Creature
        {
            public static NormalPlayer NormalPlayer = new NormalPlayer();

        }
        public static class Vegetation
        {
            public static Tree_Pine_Huge Tree_Pine_Huge = new Tree_Pine_Huge();

        }
    }
}
