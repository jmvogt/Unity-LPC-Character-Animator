using System;

namespace Assets.Scripts.Types
{
    public static class DNABlockType
    {
        public const string Body = "BODY";
        public const string Face = "FACE";
        public const string Hair = "HAIR";
        public const string Head = "HEAD";
        public const string FacialHair = "FACIALHAIR";
        public const string Ears = "EARS";
        public const string Eyes = "EYES";
        public const string Nose = "NOSE";
        public const string Neck = "NECK";
        public const string Chest = "CHEST";
        public const string Shoulder = "SHOULDER";
        public const string Arms = "ARMS";
        public const string Wrists = "WRISTS";
        public const string Hands = "HANDS";
        public const string Back = "BACK";
        public const string Back2 = "BACK2";
        public const string Waist = "WAIST";
        public const string Legs = "LEGS";
        public const string Feet = "FEET";
        public const string RightHand = "RIGHTHAND";
        public const string LeftHand = "LEFTHAND";

        public static readonly string[] TypeList = {
            Back2, Back, Body, Ears,
            Eyes, Nose, FacialHair, Face, Hair,
            Neck, Shoulder, Waist, Wrists,
            Feet, Hands, Head, Legs,
            LeftHand, Arms, Chest, RightHand
        };

        public static int GetSortingOrder(string blockType, string direction)
        {
            var index = Array.IndexOf(TypeList, blockType);
            if (direction == DirectionType.Up &&
                (blockType == Back || blockType == Back2))
            {
                // BACK2 will be on top of BACK when facing up
                return (TypeList.Length - index) * 100;
            }

            return index;
        }
    }
}
