using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class ActionAnimationStore
    {
        private static ActionAnimationStore instance;

        private ActionAnimationStore() {
            _animationCache = new Dictionary<string, Animation>();
        }

           public static ActionAnimationStore Instance
           {
              get 
              {
                 if (instance == null)
                 {
                    instance = new ActionAnimationStore();
                 }
                 return instance;
              }
           }
           private Dictionary<string, Animation> _animationCache;

        public Animation Get(string animationKey) {
            Animation returnVal;
            _animationCache.TryGetValue(animationKey, out returnVal);
            return returnVal;
        }

        public void Add(string animationKey, Animation animation) {
            _animationCache[animationKey] = animation;
        }

    }
}
