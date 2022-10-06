using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models {
    [Serializable]
    internal class LPCCharacterModelLayer {
        public int zPosition;
        public Dictionary<string, Dictionary<string, LPCRaceTypeVariant>> RaceVariantMap;
    }
}
