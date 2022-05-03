using System; 
using LOC_NETWORK.GameItems.Base;
//using System.Numerics;
using UnityEngine;

namespace LOC_NETWORK.GameItems.Base
{
    public class ItemSetting
    {

        public int ItemId;


        public Vector3 Size;

        public bool CanWalk;
        public bool CanSwim;
        public bool CanFly; 

        public float MaxRunSpeedInASecond;
        public float MaxFlySpeedInASecond;
        public float MaxSwimSpeedInASecond;

        public float MaxInteractionRange;
        public bool CanSelfInteract { get; set; }


        public string Name;

        public Element[] ElementsMakeItWeak { get; set; }
        public Element[] ElementsMakeItStrong { get; set; }

        public string Color { get; set; }
        public int SmellLevel { get; set; }
        public int Weight { get; set; } 
        public int BuoyantLevel { get; set; }

        public bool IsAlive { get; set; }
        public bool CanMove { get; set; }
        public bool CanEat { get; set; }
        public bool CanBurn { get; set; }
        public bool CanBreak { get; set; }
        public bool CanRecover { get; set; }



        public int MaxPossibleTemperature { get; set; }
        public int MinPossibleTemperature { get; set; }
        public int MaxPossibleHealth { get; set; }
        public int MaxPossiblePoisonLevel { get; set; }
        public int MaxPossibleRecoverTimes { get; set; }


        public bool IsInvisible { get; set; }
        public bool CanBeGhost { get; set; }
        public bool IsGhost { get; set; }

        public bool CanRecyle { get; set; }
        public int RecycleRate { get; set; }


        public ElementContent[] Ingredients { get; set; } 

    }
}
