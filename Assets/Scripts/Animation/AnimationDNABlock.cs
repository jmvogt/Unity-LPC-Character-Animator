using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation {
    public class AnimationDNABlock {
        
        private string _animationKey;
        private List<Sprite> _spriteList;
        private string _direction;
        private Color _spriteColor;
        private bool _enabled;
        private int _sortingOrder;

        public string AnimationKey {
            get { return _animationKey; }
        }

        public List<Sprite> SpriteList {
            get { return _spriteList; }
        }

        public int SortingOrder {
            get { return _sortingOrder; }
        }

        public string Direction {
            get { return _direction; }
            set { _direction = value; }
        }

        public Color SpriteColor {
            get { return _spriteColor; }
        }

        public bool Enabled { get { return _enabled; } set { _enabled = value; } }

        public void UpdateSpriteColor(Color spriteColor) {
            _spriteColor = spriteColor;
        }

        public AnimationDNABlock(string animationKey, List<Sprite> spriteList, string direction, int sortingOrder) {
            _animationKey = animationKey;
            _spriteList = spriteList;
            _direction = direction;
            _sortingOrder = sortingOrder;
            _enabled = true;
        }

        public AnimationDNABlock() {
            _animationKey = "UNKNOWN";
            _direction = DirectionType.NONE;
            _enabled = false;
            _sortingOrder = -99;
        }
    }
}
