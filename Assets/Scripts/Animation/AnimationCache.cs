using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation
{
    public class AnimationCache
    {
        private static AnimationCache instance;

        private AnimationCache() {
            _cacheLookup = new Dictionary<string, Dictionary<string, AnimationDNABlock>>();
            foreach(string blockKey in DNABlockType.GetTypeList()) {
                string cacheKey = blockKey.ToLower();
                _cacheLookup[cacheKey] = new Dictionary<string, AnimationDNABlock>();
            }
        }

        private Dictionary<string, Dictionary<string, AnimationDNABlock>> _cacheLookup;            

        public static AnimationCache Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AnimationCache();
                }
                return instance;
            }
        }

        public AnimationDNABlock Get(string animationKey) {
            AnimationDNABlock returnVal;
            string cacheKey = animationKey.Split('_')[0];
            Dictionary<string, AnimationDNABlock> cache = _cacheLookup[cacheKey];
            cache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, AnimationDNABlock animation) {
            string cacheKey = animationKey.Split('_')[0];
            Dictionary<string, AnimationDNABlock> cache = _cacheLookup[cacheKey];
            cache[animationKey] = animation;
        }
    }
}
