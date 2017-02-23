using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Types {
    public static class DNABlockType {
        public const string BODY = "BODY";
        public const string HAIR = "HAIR";
        public const string HEAD = "HEAD";
        public const string FACIALHAIR = "FACIALHAIR";
        public const string EARS = "EARS";
        public const string EYES = "EYES";
        public const string NOSE = "NOSE";
        public const string NECK = "NECK";
        public const string CHEST = "CHEST";
        public const string SHOULDER = "SHOULDER";
        public const string ARMS = "ARMS";
        public const string WRISTS = "WRISTS";
        public const string HANDS = "HANDS";
        public const string BACK = "BACK";
        public const string BACK2 = "BACK2";
        public const string WAIST = "WAIST";
        public const string LEGS = "LEGS";
        public const string FEET = "FEET";
        public const string RIGHTHAND = "RIGHTHAND";
        public const string LEFTHAND = "LEFTHAND";

        public static string[] GetTypeList() {

            // Objects will be layered using the index order below. 
            // For example, your weapons will render above your body.
            return new string[20] {
                BACK2, BACK, BODY, EARS, 
                EYES, NOSE, FACIALHAIR, HAIR, 
                NECK, SHOULDER, WAIST, WRISTS, 
                FEET, HANDS, HEAD, LEGS,
                LEFTHAND, ARMS, CHEST, RIGHTHAND
            };
        }

        public static int GetSortingOrder(string blockType, string direction) {
            string[] typeList = GetTypeList();
            int index = Array.IndexOf(typeList, blockType);
            if (direction == DirectionType.UP &&
                (blockType == DNABlockType.BACK || blockType == DNABlockType.BACK2)) {
                    // BACK2 will be on top of BACK when facing up
                    return (typeList.Length - index) * 100;
            } else {
                return index;
            }
        }
    }
}
