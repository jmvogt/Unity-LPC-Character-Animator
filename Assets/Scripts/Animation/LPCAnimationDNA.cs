using Assets.Scripts.Animation;
using Assets.Scripts.Animation.DNABlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    public class LPCAnimationDNA
    {
        // TODO: Use reflection to clean up this class.

        public BaseAnimationDNABlock BodyAnimation { get; set; }
        public BaseAnimationDNABlock HairAnimation { get; set; }
        public BaseAnimationDNABlock LegAnimation { get; set; }
        public BaseAnimationDNABlock NeckAnimation { get; set; }
        public BaseAnimationDNABlock BackAnimation { get; set; }
        public BaseAnimationDNABlock Back2Animation { get; set; }
        public BaseAnimationDNABlock WaistAnimation { get; set; }
        public BaseAnimationDNABlock FaceAnimation { get; set; }
        public BaseAnimationDNABlock FeetAnimation { get; set; }
        public BaseAnimationDNABlock HandAnimation { get; set; }
        public BaseAnimationDNABlock HeadAnimation { get; set; }
        public BaseAnimationDNABlock TorsoAnimation { get; set; }
        public BaseAnimationDNABlock PrimaryAnimation { get; set; }
        public BaseAnimationDNABlock SecondaryAnimation { get; set; }

        private enum SortingTypes {
            BACK2,
            BACK,
            BODY,
            FACIAL,
            HAIR,
            CLOTHING,
            ACCESSORIES,
            ARMOR,
            WEAPON
        }

        public Dictionary<string, BaseAnimationDNABlock> GetAnimationCache() {
            Dictionary<string, BaseAnimationDNABlock> animationCache = new Dictionary<string, BaseAnimationDNABlock>();
            if (BodyAnimation != null) {
                // TODO: Set LPC sorting order as a permanently configured list. Define up vs down order
                BodyAnimation.UpdateSortingOrder((int)SortingTypes.BODY);
                animationCache.Add("body", BodyAnimation);
            }
            if (HairAnimation != null) {
                HairAnimation.UpdateSortingOrder((int)SortingTypes.HAIR);
                animationCache.Add("hair", HairAnimation);
            }
            if (LegAnimation != null) {
                LegAnimation.UpdateSortingOrder((int)SortingTypes.ARMOR);
                animationCache.Add("legs", LegAnimation);
            }
            if (NeckAnimation != null) {
                NeckAnimation.UpdateSortingOrder((int)SortingTypes.ACCESSORIES);
                animationCache.Add("neck", NeckAnimation);
            }
            if (BackAnimation != null) {
                BackAnimation.UpdateSortingOrder((int)SortingTypes.BACK);
                animationCache.Add("back", BackAnimation);
            }
            if (Back2Animation != null) {
                Back2Animation.UpdateSortingOrder((int)SortingTypes.BACK2);
                animationCache.Add("back2", Back2Animation);
            }
            if (WaistAnimation != null) {
                WaistAnimation.UpdateSortingOrder((int)SortingTypes.ACCESSORIES);
                animationCache.Add("waist", WaistAnimation);
            }

            if (FeetAnimation != null) {
                // TODO: Fix the sorting type names.. feet could be plate or clothing, but there wont be two sets of shoes
                FeetAnimation.UpdateSortingOrder((int)SortingTypes.CLOTHING);
                animationCache.Add("feet", FeetAnimation);
            }
            if (HandAnimation != null) {
                HandAnimation.UpdateSortingOrder((int)SortingTypes.CLOTHING);
                animationCache.Add("hands", HandAnimation);
            }
            if (HeadAnimation != null) {
                HeadAnimation.UpdateSortingOrder((int)SortingTypes.CLOTHING);
                animationCache.Add("head", HeadAnimation);
            }
            if (TorsoAnimation != null) {
                TorsoAnimation.UpdateSortingOrder((int)SortingTypes.CLOTHING);
                animationCache.Add("torso", TorsoAnimation);
            }
            if (PrimaryAnimation != null) {
                PrimaryAnimation.UpdateSortingOrder((int)SortingTypes.WEAPON);
                animationCache.Add("primary", PrimaryAnimation);
            }
            if (SecondaryAnimation != null) {
                SecondaryAnimation.UpdateSortingOrder((int)SortingTypes.WEAPON);
                animationCache.Add("secondary", SecondaryAnimation);
            }
            return animationCache;
        }
    }
}