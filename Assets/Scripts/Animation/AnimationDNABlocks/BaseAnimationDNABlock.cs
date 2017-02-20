using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation.DNABlocks {
    public class BaseAnimationDNABlock {
        // TODO: Setup children classes to hold properties such as sorting layer
        private string _animationKey;
        private int _sortingOrder;
        private List<Sprite> _spriteList;
        private BaseAnimationDirection _animationDirection;
        private Color _spriteColor;

        public string AnimationKey {
            get { return _animationKey; }
        }
        public int SortingOrder {
            get { return _sortingOrder; }
        }
        public List<Sprite> SpriteList {
            get { return _spriteList; }
        }
        public BaseAnimationDirection AnimationDirection {
            get { return _animationDirection; }
        }

        public Color SpriteColor {
            get { return _spriteColor; }
        }

        // TODO: Get rid of this function as soon as a Template sort of mechanism is used to build DNA blocks..
        public void UpdateSortingOrder (int sortingOrder) {
            _sortingOrder = sortingOrder;
        }

        // TODO: Get this of this function as soon as responsibility of generating animationDNA
        //is given to LPCActionAnimationManager...
        public void UpdateSpriteColor(Color spriteColor) {
            _spriteColor = spriteColor;
        }

        public BaseAnimationDNABlock(string animationKey, List<Sprite> spriteList, BaseAnimationDirection animationDirection) {
            _animationKey = animationKey;
            _spriteList = spriteList;
            _animationDirection = animationDirection;
        }
        public BaseAnimationDNABlock(string animationKey, List<Sprite> spriteList, BaseAnimationDirection animationDirection, Color spriteColor) {
            _animationKey = animationKey;
            _spriteList = spriteList;
            _animationDirection = animationDirection;
            _spriteColor = spriteColor;
        }
    }
}
