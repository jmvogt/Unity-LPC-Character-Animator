using Assets.Scripts.Animation.AnimationDirections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation {
    public class AnimationDNABlock {
        // TODO: Setup children classes to hold properties such as sorting layer
        private string _animationKey;
        private List<Sprite> _spriteList;
        private BaseAnimationDirection _animationDirection;
        private Color _spriteColor;
        private bool _enabled;

        public string AnimationKey {
            get { return _animationKey; }
        }

        public List<Sprite> SpriteList {
            get { return _spriteList; }
        }

        public BaseAnimationDirection AnimationDirection {
            get { return _animationDirection; }
            set { _animationDirection = value; }
        }

        public Color SpriteColor {
            get { return _spriteColor; }
        }

        public bool Enabled { get { return _enabled; } set { _enabled = value; } }

        public void UpdateSpriteColor(Color spriteColor) {
            _spriteColor = spriteColor;
        }

        public AnimationDNABlock(string animationKey, List<Sprite> spriteList, BaseAnimationDirection animationDirection) {
            _animationKey = animationKey;
            _spriteList = spriteList;
            _animationDirection = animationDirection;
            _enabled = true;
        }
        public AnimationDNABlock(string animationKey, List<Sprite> spriteList, BaseAnimationDirection animationDirection, Color spriteColor) {
            _animationKey = animationKey;
            _spriteList = spriteList;
            _animationDirection = animationDirection;
            _spriteColor = spriteColor;
            _enabled = true;
        }

        public AnimationDNABlock() {
            _animationKey = "UNKNOWN";
            _animationDirection = new NoAnimationDirection();
            _enabled = false;
        }
    }
}
