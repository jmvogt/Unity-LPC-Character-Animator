using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models {
    [Serializable]
    internal class LPCCharacterLayerAssetDefinition {
        public string Name;
        public short zPosition;
        public Dictionary<string, LPCCharacterAssetRaceVariantMap> RaceVariantMap;
    }
}
