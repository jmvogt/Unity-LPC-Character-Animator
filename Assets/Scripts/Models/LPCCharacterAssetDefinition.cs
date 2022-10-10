using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models {
    [Serializable]
    internal class LPCCharacterAssetDefinition {
        public string Name;
        public string Type;
        public Dictionary<string, LPCCharacterLayerAssetDefinition> Layers;
    }
}
