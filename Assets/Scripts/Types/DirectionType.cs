using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Types {
    public static class DirectionType {
        public const string NONE = "NONE";
        public const string LEFT = "LEFT";
        public const string RIGHT = "RIGHT";
        public const string UP = "UP";
        public const string DOWN = "DOWN";

        public static string GetAnimationForDirection(string direction) {
            switch (direction) {
                case "LEFT":
                    return "l";
                case "RIGHT":
                    return "r";
                case "UP":
                    return "t";
                case "DOWN":
                    return "d";
                default:
                    return "";
            }
        }
    }
}
