using Assets.Scripts.Animation.ActionAnimations;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    
    class ActionAnimationManager
    {
        //take in player DNA, and build the corresponding sprite list. This should first check a cache
        //and if its not found, then use the AnimationActionImporter to fetch/build

        public AnimationDNA BuildDNAForAction(CharacterDNA characterDNA, IAction actionAnimation) {
            AnimationDNA animationDNA = new AnimationDNA();
            if (characterDNA.BackDNA != null)
                animationDNA.BackAnimation = getAnimation(characterDNA.BackDNA.ItemKey, actionAnimation);
            if (characterDNA.Back2DNA != null)
                animationDNA.Back2Animation = getAnimation(characterDNA.Back2DNA.ItemKey, actionAnimation);
            if (characterDNA.BodyDNA != null)
                animationDNA.BodyAnimation = getAnimation(characterDNA.BodyDNA.ItemKey, actionAnimation);
            if (characterDNA.FaceDNA != null)
                animationDNA.FaceAnimation = getAnimation(characterDNA.FaceDNA.ItemKey, actionAnimation);
            if (characterDNA.FeetDNA != null)
                animationDNA.FeetAnimation = getAnimation(characterDNA.FeetDNA.ItemKey, actionAnimation);
            if (characterDNA.HairDNA != null)
                animationDNA.HairAnimation = getAnimation(characterDNA.HairDNA.ItemKey, actionAnimation);
            if (characterDNA.HandDNA != null)
                animationDNA.HandAnimation = getAnimation(characterDNA.HandDNA.ItemKey, actionAnimation);
            if (characterDNA.HeadDNA != null)
                animationDNA.HeadAnimation = getAnimation(characterDNA.HeadDNA.ItemKey, actionAnimation);
            if (characterDNA.LegDNA != null)
                animationDNA.LegAnimation = getAnimation(characterDNA.LegDNA.ItemKey, actionAnimation);
            if (characterDNA.NeckDNA != null)
                animationDNA.NeckAnimation = getAnimation(characterDNA.NeckDNA.ItemKey, actionAnimation);
            if (characterDNA.PrimaryDNA != null)
                animationDNA.PrimaryAnimation = getAnimation(characterDNA.PrimaryDNA.ItemKey, actionAnimation);
            if (characterDNA.SecondaryDNA != null)
                animationDNA.SecondaryAnimation = getAnimation(characterDNA.SecondaryDNA.ItemKey, actionAnimation);
            if (characterDNA.TorsoDNA != null)
                animationDNA.TorsoAnimation = getAnimation(characterDNA.TorsoDNA.ItemKey, actionAnimation);
            if (characterDNA.WaistDNA != null)
                animationDNA.WaistAnimation = getAnimation(characterDNA.WaistDNA.ItemKey, actionAnimation);
            return animationDNA;
        }

        private Animation getAnimation(string animationKey, IAction actionAnimation) {
            ActionAnimationStore animationStore = ActionAnimationStore.Instance;
            Animation animation = animationStore.Get(animationKey);
            if (animation == null) {
                IAnimationImporter importer = actionAnimation.GetAnimationImporter();
                List<Animation> newAnimations = importer.ImportAnimations(animationKey);
                foreach (Animation newAnimation in newAnimations)
                    animationStore.Add(animationKey, newAnimation);
                animation = animationStore.Get(animationKey);
            }
            return animation;
        }
    }
}
