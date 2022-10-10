using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models {
    [Serializable]
    internal class LPCCharacterAssetRaceVariantMap {
        public Dictionary<string, LPCCharacterRaceVariantMap> VariantMap;
    }
}
