using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class SlashAction : BaseAction {
        public override IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter(
                String.Format("{0}_d", GetAnimationTag()), _numberOfFrames, 86, false);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter(
                String.Format("{0}_l", GetAnimationTag()), _numberOfFrames, 92, false);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter(
                String.Format("{0}_r", GetAnimationTag()), _numberOfFrames, 98, false);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter(
                String.Format("{0}_t", GetAnimationTag()), _numberOfFrames, 104, false);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public SlashAction() : base(6) {
        }

        public override string GetAnimationTag() {
            return "sl";
        }

        public override string GetAnimationType() {
            return "slash";
        }
    }
}