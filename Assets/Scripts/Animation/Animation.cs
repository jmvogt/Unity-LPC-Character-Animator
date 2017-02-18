using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation {
    public class Animation {
        private string _animationKey;
        private List<Sprite> _spriteList;

        public string AnimationKey {
            get { return _animationKey; }
        }
        List<Sprite> SpriteList {
            get { return _spriteList; }
        }

        public Animation(string animationKey, List<Sprite> spriteList) {
            _animationKey = animationKey;
            _spriteList = spriteList;
        }
    }
}
