using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models {
    [Serializable]
    internal class LPCCharacterModel {
        public string Name;
        public string Type;
        public Dictionary<string, LPCRaceTypeVariant> Variants;
    }
}
