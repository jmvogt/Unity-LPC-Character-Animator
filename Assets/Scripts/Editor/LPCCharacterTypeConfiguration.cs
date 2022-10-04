using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor {
    public class LPCCharacterTypeConfiguration : ScriptableObject {
        [SerializeField]
        public string Name;

        [SerializeField]
        public string Type;

        [SerializeField]
        public List<KeyValuePair<string, LPCCharacterTypeLayer>> Layers = new();

        public Dictionary<string, LPCCharacterTypeLayer> LayersDict = new();

        //[SerializeField]
        //public List<string> Variants;

        private void Awake() {
            foreach(var layer in Layers) {
                LayersDict.Add(layer.Key, layer.Value);
            }
        }
    }
}
