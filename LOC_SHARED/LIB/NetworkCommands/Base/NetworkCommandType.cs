using System;
namespace LOC_SHARED.NetworkItems
{
    public class NetworkCommandType
    {
        public static class SystemCommands
        {
            public static byte CommandGroup = 1;

            public static byte None = 10;
            public static byte FirstConnection = 11;
            public static byte ConnectionConfirmation = 12;
            public static byte ReceiveConfirmation = 13;
            public static byte Ping = 14;
            public static byte Pong = 15;
        }


        public static class MainCommands
        {
            public static byte CommandGroup = 2;
            public static byte MainCommand_MultipleData = 22;  //rot, pos 
        }
            //rot, pos  in every command

            public static class PlayerActionCommands
        { 
            public static byte CommandGroup = 3;

            public static byte PlayerData_ReBorn = 15;  //rot, pos 

            public static byte PlayerData_Position = 15;  //rot, pos 

            public static byte PlayerData_ArmWeapon = 15; // itemid
            public static byte PlayerData_ArmBullet = 15; //  itemid
            public static byte PlayerData_ChangeWeapon = 15; //  itemid
            public static byte PlayerData_DisarmWeapon = 15; //  itemid
            public static byte PlayerData_Hit = 15; // hitto
            public static byte PlayerData_Block = 15;  // 
            public static byte PlayerData_GetHit = 15;  //  gethitfrom, gethitpart
            public static byte PlayerData_Heal = 15; //  heal
            public static byte PlayerData_Died = 15; //rot, pos

            public static byte PlayerData_Walk = 15; // 
            public static byte PlayerData_Run = 15;// 
            public static byte PlayerData_Jump = 15;//
            public static byte PlayerData_Crouch = 15; // 
            public static byte PlayerData_EndCrouch = 15;// 
            public static byte PlayerData_Aim = 15;// 
            public static byte PlayerData_EndAim = 15;// 
            public static byte PlayerData_SwitchHand = 15;// 
            public static byte PlayerData_BeginRide = 15;// animal
            public static byte PlayerData_EndRide = 15;// animal
        }

        public static class PlayerInventoryCommands
        { 
            public static byte CommandGroup = 4;

            public static byte PlayerData_Inventory_AddNewItem = 15; //  itemId, UID
            public static byte PlayerData_Inventory_ThrowItem = 15; // itemId, UID, itempos, itemrot
            public static byte PlayerData_Inventory_TakeItem = 15; //  itemId, UID, itempos, itemrot
            public static byte PlayerData_Inventory_ChangeBox = 15; //  itemId, fromBox, toBox
        }


    }
}
