using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.DNABlocks;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    
    class LPCActionAnimationManager
    {
        // TODO: Use reflection to shorten up this method
        public LPCAnimationDNA BuildDNAForAction(LPCCharacterDNA characterDNA, BaseAction actionAnimation, BaseAnimationDirection direction) {
            LPCAnimationDNA animationDNA = new LPCAnimationDNA();
            if (characterDNA.BackDNA != null)
                animationDNA.BackAnimation = getAnimation(characterDNA.BackDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.Back2DNA != null)
                animationDNA.Back2Animation = getAnimation(characterDNA.Back2DNA.ItemKey, actionAnimation, direction);
            if (characterDNA.BodyDNA != null)
                animationDNA.BodyAnimation = getAnimation(characterDNA.BodyDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.FaceDNA != null)
                animationDNA.FaceAnimation = getAnimation(characterDNA.FaceDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.FeetDNA != null)
                animationDNA.FeetAnimation = getAnimation(characterDNA.FeetDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.HairDNA != null)
                animationDNA.HairAnimation = getAnimation(characterDNA.HairDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.HandDNA != null)
                animationDNA.HandAnimation = getAnimation(characterDNA.HandDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.HeadDNA != null)
                animationDNA.HeadAnimation = getAnimation(characterDNA.HeadDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.LegDNA != null)
                animationDNA.LegAnimation = getAnimation(characterDNA.LegDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.NeckDNA != null)
                animationDNA.NeckAnimation = getAnimation(characterDNA.NeckDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.PrimaryDNA != null)
                animationDNA.PrimaryAnimation = getAnimation(characterDNA.PrimaryDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.SecondaryDNA != null)
                animationDNA.SecondaryAnimation = getAnimation(characterDNA.SecondaryDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.TorsoDNA != null)
                animationDNA.TorsoAnimation = getAnimation(characterDNA.TorsoDNA.ItemKey, actionAnimation, direction);
            if (characterDNA.WaistDNA != null)
                animationDNA.WaistAnimation = getAnimation(characterDNA.WaistDNA.ItemKey, actionAnimation, direction);
            return animationDNA;
        }

        private BaseAnimationDNABlock getAnimation(string modelKey, BaseAction actionAnimation, BaseAnimationDirection direction) {
            LPCActionAnimationCache animationStore = LPCActionAnimationCache.Instance;
            
            string animationKey = modelKey;
            if (direction != null) {
                animationKey = String.Format("{0}_{1}_{2}", 
                    animationKey, 
                    actionAnimation.GetAnimationTag(), 
                    direction.GetAnimationDirection()); 
            }

            BaseAnimationDNABlock returnAnimation = animationStore.Get(animationKey);
            if (returnAnimation == null) {
                IAnimationImporter importer = actionAnimation.GetAnimationImporter();
                List<BaseAnimationDNABlock> newAnimations = importer.ImportAnimations(modelKey, direction);

                foreach (BaseAnimationDNABlock newAnimation in newAnimations) {
                    if (newAnimation.AnimationDirection != null) {
                        animationKey = String.Format("{0}_{1}_{2}",
                            modelKey,
                            actionAnimation.GetAnimationTag(),
                            newAnimation.AnimationDirection.GetAnimationDirection());
                        
                        if (newAnimation.AnimationDirection == direction) {
                            returnAnimation = newAnimation;
                        }
                    }

                    animationStore.Add(animationKey, newAnimation);
                }
                    
            }
            return returnAnimation;
        }
    }
}
