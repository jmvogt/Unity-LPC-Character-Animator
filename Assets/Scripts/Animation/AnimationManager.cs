using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
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
        public void UpdateDNAForAction(CharacterDNA characterDNA, AnimationDNA animationDNA, BaseAction actionAnimation, BaseAnimationDirection newDirection) {
            foreach (string blockType in DNABlockType.GetTypeList()) {
                CharacterDNABlock characterDNABlock = characterDNA.DNABlocks[blockType];
                if (characterDNABlock.Enabled && characterDNABlock.IsDirty) {
                    animationDNA.DNABlocks[blockType] = getAnimation(characterDNABlock.ModelKey, actionAnimation, newDirection);
                    AnimationDNABlock animationDNABlock = animationDNA.DNABlocks[blockType];
                    animationDNABlock.UpdateSpriteColor(characterDNABlock.ItemColor);
                    animationDNABlock.Enabled = true;
                    //characterDNABlock.IsDirty = false;
                }
            }
        }

        private AnimationDNABlock getAnimation(string modelKey, BaseAction actionAnimation, BaseAnimationDirection direction) {
            AnimationCache animationStore = AnimationCache.Instance;
            
            string animationKey = modelKey;
            if (direction != null && direction.GetAnimationDirection().Length > 0) {
                animationKey = String.Format("{0}_{1}_{2}", 
                    animationKey, 
                    actionAnimation.GetAnimationTag(), 
                    direction.GetAnimationDirection());
            } else {
                animationKey = String.Format("{0}_{1}",
                    animationKey,
                    actionAnimation.GetAnimationTag());
            }

            return animationStore.Get(animationKey);
        }

        public void LoadAllAnimationsIntoCache() {
            List<string> modelList = AtlasManager.GetModelList();

            for (int i = 0; i < modelList.Count; i++) {
                LoadAnimationIntoCache(modelList[i]);
                AtlasManager.IncrementModelLoaded();
            }
        }

        public void LoadAnimationIntoCache(string modelKey) {
            AnimationCache animationStore = AnimationCache.Instance;
            List<BaseAction> actionsToImport = new List<BaseAction>();
            actionsToImport.Add(new SlashAction());
            actionsToImport.Add(new SpellcastAction());
            actionsToImport.Add(new ThrustAction());
            actionsToImport.Add(new WalkAction());

            foreach (BaseAction actionAnimation in actionsToImport) { 
                IAnimationImporter importer = actionAnimation.GetAnimationImporter();
                List<AnimationDNABlock> newAnimations = importer.ImportAnimations(modelKey, new DownAnimationDirection());

                foreach (AnimationDNABlock newAnimation in newAnimations) {
                    string animationKey = String.Format("{0}_{1}_{2}",
                            modelKey,
                            actionAnimation.GetAnimationTag(),
                            newAnimation.AnimationDirection.GetAnimationDirection());
                    animationStore.Add(animationKey, newAnimation);
                }
            }

            BaseAction deathAnimation = new DeathAction();
            IAnimationImporter deathImporter = deathAnimation.GetAnimationImporter();
            List<AnimationDNABlock> deathAnimations = deathImporter.ImportAnimations(modelKey, null);

            foreach (AnimationDNABlock newAnimation in deathAnimations) {
                string animationKey = String.Format("{0}_{1}",
                        modelKey,
                        deathAnimation.GetAnimationTag());
                animationStore.Add(animationKey, newAnimation);
            }

        }
    }
}
