using Assets.Scripts.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    public class AnimationDNA
    {
        private Dictionary<string, AnimationDNABlock> _dnaBlocks;

        public Dictionary<string, AnimationDNABlock> DNABlocks { get { return _dnaBlocks; } set { _dnaBlocks = value; } }

        public AnimationDNA() {
            _dnaBlocks = new Dictionary<string, AnimationDNABlock>();
            foreach (string blockKey in DNABlockType.GetTypeList()) {
                _dnaBlocks.Add(blockKey, new AnimationDNABlock());
            }
        }
        
        private enum SortingTypes {
            BACK2,
            BACK,
            BODY,
            EARS,
            EYES,
            NOSE,
            ACCESSORIES,
            FACIALHAIR,
            HAIR,
            NECK,
            SHOULDER,
            WAIST,
            WRISTS,
            FEET,
            HANDS,
            HEAD,
            LEGS,
            LEFTHAND,
            ARMS,
            CHEST,
            RIGHTHAND
        }

        public int GetSortingOrder(string blockKey) {
            switch (blockKey) {
                case "BODY":
                    return (int)SortingTypes.BODY;
                case "HAIR":
                    return (int)SortingTypes.HAIR;
                case "HEAD":
                    return (int)SortingTypes.HEAD;
                case "FACIALHAIR":
                    return (int)SortingTypes.FACIALHAIR;
                case "EARS":
                    return (int)SortingTypes.EARS;
                case "EYES":
                    return (int)SortingTypes.EYES;
                case "NOSE":
                    return (int)SortingTypes.NOSE;
                case "NECK":
                    return (int)SortingTypes.NECK;
                case "CHEST":
                    return (int)SortingTypes.CHEST;
                case "SHOULDER":
                    return (int)SortingTypes.SHOULDER;
                case "ARMS":
                    return (int)SortingTypes.ARMS;
                case "WRISTS":
                    return (int)SortingTypes.WRISTS;
                case "HANDS":
                    return (int)SortingTypes.HANDS;
                case "BACK":
                    return (int)SortingTypes.BACK;
                case "BACK2":
                    return (int)SortingTypes.BACK2;
                case "WAIST":
                    return (int)SortingTypes.WAIST;
                case "LEGS":
                    return (int)SortingTypes.LEGS;
                case "FEET":
                    return (int)SortingTypes.FEET;
                case "RIGHTHAND":
                    return (int)SortingTypes.RIGHTHAND;
                case "LEFTHAND":
                    return (int)SortingTypes.LEFTHAND;
                    
             }
            return -99; //error

        }

    }
}