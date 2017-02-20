using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class LPCCharacterDNABlock {
        public string ItemKey { get; set; }

        public Color ItemColor { get; set; }

        public LPCCharacterDNABlock(string itemKey) {
            ItemKey = itemKey;
            ItemColor = new Color();
        }

        public LPCCharacterDNABlock(string itemKey, Color itemColor) {
            ItemKey = itemKey;
            ItemColor = itemColor;
        }
    }
}
