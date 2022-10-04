using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor {

    [Serializable]
    public class LPCCharacterTypeVariantSpriteMap {
        [SerializeField]
        public List<KeyValuePair<string, Texture2D>> VariantSpriteList = new();

        public Dictionary<string, Texture2D> VariantSpriteDict = new();

        private void Awake() {
            foreach (var map in VariantSpriteList) {
                VariantSpriteDict.Add(map.Key, map.Value);
            }
        }
    }

    [Serializable]
    public class LPCCharacterTypeLayer {
        public int zPosition;

        [SerializeField]
        public List<KeyValuePair<string, LPCCharacterTypeVariantSpriteMap>> RaceVariantSpritePathList = new();

        public Dictionary<string, LPCCharacterTypeVariantSpriteMap> RaceVariantSpritePathDict = new();

        private void Awake() {
            foreach (var map in RaceVariantSpritePathList) {
                RaceVariantSpritePathDict.Add(map.Key, map.Value);
            }
        }
    }
}
