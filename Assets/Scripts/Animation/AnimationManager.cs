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
            foreach (string blockType in DNABlockType.GetTypeList()) {
                CharacterDNABlock characterDNABlock = characterDNA.DNABlocks[blockType];
                if (characterDNABlock.Enabled) {
                    animationDNA.DNABlocks[blockType] = getAnimation(characterDNABlock.ModelKey, actionAnimation, newDirection);
                    AnimationDNABlock animationDNABlock = animationDNA.DNABlocks[blockType];
                    animationDNABlock.UpdateSpriteColor(characterDNABlock.ItemColor);
                    animationDNABlock.Enabled = true;
                }
                characterDNABlock.IsDirty = false;
            }
        }

        private AnimationDNABlock getAnimation(string modelKey, BaseAction actionAnimation, string direction) {
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
            actionsToImport.Add(new ShootAction());

            foreach (BaseAction actionAnimation in actionsToImport) { 
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
