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
    class LPCActionAnimationCache
    {
        private static LPCActionAnimationCache instance;

        private LPCActionAnimationCache() {
            
            Dictionary<string, BaseAnimationDNABlock> _bodyCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _facialCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _hairCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _armsCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _backCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _back2Cache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _chestCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _feetCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _handsCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _headCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _legsCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _neckCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _shoulderCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _waistCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _weaponsCache = new Dictionary<string,BaseAnimationDNABlock>();
            Dictionary<string, BaseAnimationDNABlock> _wristsCache = new Dictionary<string,BaseAnimationDNABlock>();

            _cacheLookup["body"] = _bodyCache;
            _cacheLookup["facial"] = _facialCache;
            _cacheLookup["hair"] = _hairCache;
            _cacheLookup["arms"] = _armsCache;
            _cacheLookup["back"] = _backCache;
            _cacheLookup["back2"] = _back2Cache;
            _cacheLookup["chest"] = _chestCache;
            _cacheLookup["feet"] = _feetCache;
            _cacheLookup["hands"] = _handsCache;
            _cacheLookup["head"] = _headCache;
            _cacheLookup["legs"] = _legsCache;
            _cacheLookup["neck"] = _neckCache;
            _cacheLookup["shoulder"] = _shoulderCache;
            _cacheLookup["waist"] = _waistCache;
            _cacheLookup["weapons"] = _weaponsCache;
            _cacheLookup["wrists"] = _wristsCache;
        }

        private Dictionary<string, Dictionary<string, BaseAnimationDNABlock>> _cacheLookup = 
            new Dictionary<string, Dictionary<string, BaseAnimationDNABlock>>();

        public static LPCActionAnimationCache Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new LPCActionAnimationCache();
                }
                return instance;
            }
        }

        private Dictionary<string, BaseAnimationDNABlock> _bodyCache;
        private Dictionary<string, BaseAnimationDNABlock> _facialCache;
        private Dictionary<string, BaseAnimationDNABlock> _hairCache;
        private Dictionary<string, BaseAnimationDNABlock> _armsCache;
        private Dictionary<string, BaseAnimationDNABlock> _backCache;
        private Dictionary<string, BaseAnimationDNABlock> _back2Cache;
        private Dictionary<string, BaseAnimationDNABlock> _chestCache;
        private Dictionary<string, BaseAnimationDNABlock> _feetCache;
        private Dictionary<string, BaseAnimationDNABlock> _handsCache;
        private Dictionary<string, BaseAnimationDNABlock> _headCache;
        private Dictionary<string, BaseAnimationDNABlock> _legsCache;
        private Dictionary<string, BaseAnimationDNABlock> _neckCache;
        private Dictionary<string, BaseAnimationDNABlock> _shoulderCache;
        private Dictionary<string, BaseAnimationDNABlock> _waistCache;
        private Dictionary<string, BaseAnimationDNABlock> _weaponsCache;
        private Dictionary<string, BaseAnimationDNABlock> _wristsCache;

        public BaseAnimationDNABlock Get(string animationKey) {
            BaseAnimationDNABlock returnVal;
            string cacheKey = animationKey.Split('_')[0];
            Dictionary<string, BaseAnimationDNABlock> cache = _cacheLookup[cacheKey];
            cache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, BaseAnimationDNABlock animation) {
            string cacheKey = animationKey.Split('_')[0];
            Dictionary<string, BaseAnimationDNABlock> cache = _cacheLookup[cacheKey];
            cache[animationKey] = animation;
        }
    }
}
