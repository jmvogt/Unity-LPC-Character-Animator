using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Animation.DNABlocks;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class ActionAnimationCache
    {
        private static ActionAnimationCache instance;

        private ActionAnimationCache() {
            _animationCache = new Dictionary<string, BaseAnimationDNABlock>();
        }

        public static ActionAnimationCache Instance
        {
            get 
            {
                if (instance == null)
                {
                instance = new ActionAnimationCache();
                }
                return instance;
            }
        }

        private Dictionary<string, BaseAnimationDNABlock> _animationCache;

        public BaseAnimationDNABlock Get(string animationKey) {
            BaseAnimationDNABlock returnVal;
            _animationCache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, BaseAnimationDNABlock animation) {
            _animationCache[animationKey] = animation;
        }

    }
}
