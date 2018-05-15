using System.Collections.Generic;
using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class AnimationManager
    {
        public AnimationManager()
        {
            // All directional actions
            _directionalActions = new List<BaseAction>
            {
                new SlashAction(),
                new SpellcastAction(),
                new ThrustAction(),
                new WalkAction(),
                new ShootAction(),
                new DeathAction()
            };
        }

        private readonly List<BaseAction> _directionalActions;

        public static void UpdateDNAForAction(CharacterDNA characterDNA, AnimationDNA animationDNA, BaseAction actionAnimation, string newDirection)
        {
            // Uses the characterDNA to fetch the proper animations and update the animationDNA.
            foreach (var blockType in DNABlockType.TypeList)
            {
                var characterDNABlock = characterDNA.DNABlocks[blockType];

                if (characterDNABlock.Enabled)
                {
                    animationDNA.DNABlocks[blockType] = GetAnimation(characterDNABlock.ModelKey, actionAnimation, newDirection);
                    var animationDNABlock = animationDNA.DNABlocks[blockType];
                    if (animationDNABlock == null)
                    {
                        Debug.Log($"Block not found: {blockType}");
                        continue;
                    }

                    animationDNABlock.UpdateSpriteColor(characterDNABlock.ItemColor);
                    animationDNABlock.Enabled = true;
                }
                else
                {
                    // Disable the animation slot if the character slot isnt enabled
                    animationDNA.DNABlocks[blockType].Enabled = false;
                }

                characterDNABlock.IsDirty = false;
            }
        }

        private static AnimationDNABlock GetAnimation(string modelKey, BaseAction actionAnimation, string direction)
        {
            // Fetches an animation from the animation store/cache
            var animationStore = AnimationCache.Instance;
            var animationKey = modelKey;
            animationKey = direction.Equals(DirectionType.None)
                ? $"{animationKey}_{actionAnimation.GetAnimationTag()}"
                : $"{animationKey}_{actionAnimation.GetAnimationTag()}_{DirectionType.GetAnimationForDirection(direction)}";

            return animationStore.Get(animationKey);
        }

        public void LoadAllAnimationsIntoCache()
        {
            //Builds all animations from sprites and adds them to the cache.
            //This should be called when a scene is FIRST loaded, before initializing characters.

            var modelList = AtlasManager.Instance.ModelList;
            foreach (var model in modelList)
            {
                LoadAnimationIntoCache(model);
                AtlasManager.Instance.IncrementModelLoaded();
            }
        }

        private void LoadAnimationIntoCache(string modelKey)
        {
            // Builds the animation object for all model, action, direction
            // combinations. Object is then added to the AnimationCache.

            var animationStore = AnimationCache.Instance;

            foreach (var actionAnimation in _directionalActions)
            {
                // Use the respective importer for the action 
                var importer = actionAnimation.GetAnimationImporter();
                var newAnimations = importer.ImportAnimations(modelKey, DirectionType.Down);

                foreach (var newAnimation in newAnimations)
                {
                    var animationKey = $"{modelKey}_{actionAnimation.GetAnimationTag()}_{DirectionType.GetAnimationForDirection(newAnimation.Direction)}";
                    animationStore.Add(animationKey, newAnimation);
                }
            }

            // The "Idle" action reuses the first image of the death animation, so we need
            // to import the first frame of all death sprites without a direction tag.
            BaseAction deathAnimation = new DeathAction();
            var deathImporter = deathAnimation.GetAnimationImporter();
            var deathAnimations = deathImporter.ImportAnimations(modelKey, DirectionType.None);

            foreach (var newAnimation in deathAnimations)
            {
                var animationKey = $"{modelKey}_{deathAnimation.GetAnimationTag()}";
                animationStore.Add(animationKey, newAnimation);
            }
        }
    }
}
