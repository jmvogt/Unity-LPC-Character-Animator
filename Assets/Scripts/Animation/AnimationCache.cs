using System.Collections.Generic;
using Assets.Scripts.Types;

namespace Assets.Scripts.Animation
{
    public class AnimationCache
    {
        private static AnimationCache _instance;

        private readonly Dictionary<string, Dictionary<string, AnimationDNABlock>> _cacheLookup;

        private AnimationCache()
        {
            _cacheLookup = new Dictionary<string, Dictionary<string, AnimationDNABlock>>();
            foreach (var blockKey in DNABlockType.TypeList)
            {
                var cacheKey = blockKey.ToLower();
                _cacheLookup[cacheKey] = new Dictionary<string, AnimationDNABlock>();
            }
        }

        public static AnimationCache Instance => _instance ?? (_instance = new AnimationCache());

        public AnimationDNABlock Get(string animationKey)
        {
            AnimationDNABlock returnVal;
            var cacheKey = animationKey.Split('_')[0];
            var cache = _cacheLookup[cacheKey];
            cache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, AnimationDNABlock animation)
        {
            var cacheKey = animationKey.Split('_')[0];
            var cache = _cacheLookup[cacheKey];
            cache[animationKey] = animation;
        }
    }
}
