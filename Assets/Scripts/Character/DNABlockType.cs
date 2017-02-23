using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character {
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
            return new string[20] {
                BODY, HAIR, HEAD, FACIALHAIR,
                EARS, EYES, NOSE, NECK,
                CHEST, SHOULDER, ARMS, WRISTS,
                HANDS, BACK, BACK2, WAIST,
                LEGS, FEET, RIGHTHAND, LEFTHAND
            };
        }
    }
}
