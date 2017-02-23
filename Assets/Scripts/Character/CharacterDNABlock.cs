using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character {

    public class CharacterDNABlock {

        private string _modelKey;
        private Color _itemColor;
        private bool _enabled;
        private bool _isDirty;

        public string ModelKey { get { return _modelKey; } set { _modelKey = value; } }

        public Color ItemColor { get { return _itemColor; } set { _itemColor = value; } }

        public bool Enabled { get { return _enabled; } set { _enabled = value; } }

        public bool IsDirty { get { return _isDirty; } set { _isDirty = value; } }

        public CharacterDNABlock() {
            _modelKey = "UNKNOWN";
            _itemColor = new Color();
            _enabled = false;
            _isDirty = false;
        }

        public CharacterDNABlock(string itemKey) {
            Update(itemKey, new Color());
        }

        public CharacterDNABlock(string itemKey, Color itemColor) {
            Update(itemKey, itemColor);
        }

        public void Update(string itemKey, Color itemColor) {
            _modelKey = itemKey;
            _itemColor = itemColor;
            _enabled = true;
            _isDirty = true;
        }

        public void UpdateColor(Color itemColor) {
            _itemColor = itemColor;
            _enabled = true;
            _isDirty = true;
        }

        public void UpdateModel(string modelKey) {
            _modelKey = modelKey;
            _enabled = true;
            _isDirty = true;
        }
    }
}
