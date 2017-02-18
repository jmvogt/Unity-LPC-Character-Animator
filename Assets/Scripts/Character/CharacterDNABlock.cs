using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character {
    public class CharacterDNABlock {
        public string ItemKey { get; set; }

        public CharacterDNABlock(string itemKey) {
            ItemKey = itemKey;
        }
    }
}
