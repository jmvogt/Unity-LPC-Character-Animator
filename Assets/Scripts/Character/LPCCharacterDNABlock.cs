using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character {
    public class LPCCharacterDNABlock {
        public string ItemKey { get; set; }

        public LPCCharacterDNABlock(string itemKey) {
            ItemKey = itemKey;
        }
    }
}
