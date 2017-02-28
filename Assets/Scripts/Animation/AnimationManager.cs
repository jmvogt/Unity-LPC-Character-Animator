using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    
    public class AnimationManager
    {
        public void UpdateDNAForAction(CharacterDNA characterDNA, AnimationDNA animationDNA, BaseAction actionAnimation, string newDirection) {
            /*
             *  Uses the characterDNA to fetch the proper animations and update the animationDNA.
             */
            foreach (string blockType in DNABlockType.GetTypeList()) {
                CharacterDNABlock characterDNABlock = characterDNA.DNABlocks[blockType];
                
                if (characterDNABlock.Enabled) {
                    animationDNA.DNABlocks[blockType] = getAnimation(characterDNABlock.ModelKey, actionAnimation, newDirection);
                    AnimationDNABlock animationDNABlock = animationDNA.DNABlocks[blockType];
                    animationDNABlock.UpdateSpriteColor(characterDNABlock.ItemColor);
                    animationDNABlock.Enabled = true;
                } else {
                    // Disable the animation slot if the character slot isnt enabled
                    animationDNA.DNABlocks[blockType].Enabled = false;
                }
                characterDNABlock.IsDirty = false;
            }
        }

        private AnimationDNABlock getAnimation(string modelKey, BaseAction actionAnimation, string direction) {
            /*
             *  Fetches an animation from the animation store/cache
             */

            AnimationCache animationStore = AnimationCache.Instance;
            string animationKey = modelKey;
            if (!direction.Equals(DirectionType.NONE)) {
                animationKey = String.Format("{0}_{1}_{2}", 
                    animationKey, 
                    actionAnimation.GetAnimationTag(),
                    DirectionType.GetAnimationForDirection(direction));
            } else {
                animationKey = String.Format("{0}_{1}",
                    animationKey,
                    actionAnimation.GetAnimationTag());
            }

            return animationStore.Get(animationKey);
        }

        public void LoadAllAnimationsIntoCache() {
            /*
             *  Builds all animations from sprites and adds them to the cache.
             *  This should be called when a scene is FIRST loaded, before initializing characters.
             */

            List<string> modelList = AtlasManager.GetModelList();
            for (int i = 0; i < modelList.Count; i++) {
                LoadAnimationIntoCache(modelList[i]);
                AtlasManager.IncrementModelLoaded();
            }
        }

        public void LoadAnimationIntoCache(string modelKey) {
            /*
             *  Builds the animation object for all model, action, direction
             *  combinations. Object is then added to the AnimationCache.
             */

            AnimationCache animationStore = AnimationCache.Instance;

            // All directional actions
            List<BaseAction> directionalActions = new List<BaseAction>();
            directionalActions.Add(new SlashAction());
            directionalActions.Add(new SpellcastAction());
            directionalActions.Add(new ThrustAction());
            directionalActions.Add(new WalkAction());
            directionalActions.Add(new ShootAction());
            directionalActions.Add(new DeathAction());

            foreach (BaseAction actionAnimation in directionalActions) { 

                // Use the respective importer for the action 
                IAnimationImporter importer = actionAnimation.GetAnimationImporter();
                List<AnimationDNABlock> newAnimations = importer.ImportAnimations(modelKey, DirectionType.DOWN);

                foreach (AnimationDNABlock newAnimation in newAnimations) {
                    string animationKey = String.Format("{0}_{1}_{2}",
                            modelKey,
                            actionAnimation.GetAnimationTag(),
                            DirectionType.GetAnimationForDirection(newAnimation.Direction));
                    animationStore.Add(animationKey, newAnimation);
                }
            }

            // The "Idle" action reuses the first image of the death animation, so we need
            // to import the first frame of all death sprites without a direction tag.
            BaseAction deathAnimation = new DeathAction();
            IAnimationImporter deathImporter = deathAnimation.GetAnimationImporter();
            List<AnimationDNABlock> deathAnimations = deathImporter.ImportAnimations(modelKey, DirectionType.NONE);

            foreach (AnimationDNABlock newAnimation in deathAnimations) {
                string animationKey = String.Format("{0}_{1}",
                        modelKey,
                        deathAnimation.GetAnimationTag());
                animationStore.Add(animationKey, newAnimation);
            }
        }
    }
}
