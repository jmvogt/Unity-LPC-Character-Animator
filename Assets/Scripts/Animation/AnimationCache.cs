using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Types;

namespace Assets.Scripts.Animation
{
    public class AnimationCache
    {
        private static AnimationCache _instance;
        private readonly Dictionary<string, Dictionary<string, AnimationDNABlock>> _cacheLookup = DNABlockType.TypeList.ToDictionary(bt => bt.ToLower(), v => new Dictionary<string, AnimationDNABlock>());
        public static AnimationCache Instance => _instance ?? (_instance = new AnimationCache());

        public AnimationDNABlock Get(string animationKey)
        {
            AnimationDNABlock returnVal;
            var cacheKey = animationKey.Split('_').FirstOrDefault();
            var cache = _cacheLookup.SafeGet(cacheKey);
            cache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, AnimationDNABlock animation)
        {
            var cacheKey = animationKey.Split('_').FirstOrDefault();
            var cache = _cacheLookup.SafeGet(cacheKey);
            cache[animationKey] = animation;
        }
    }
}
