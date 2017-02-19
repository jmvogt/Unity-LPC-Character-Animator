using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class ThrustAction : BaseAction {
        public override IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter(
                String.Format("{0}_d", GetAnimationTag()), _numberOfFrames, 110, true);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter(
                String.Format("{0}_l", GetAnimationTag()), _numberOfFrames, 118, true);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter(
                String.Format("{0}_r", GetAnimationTag()), _numberOfFrames, 126, true);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter(
                String.Format("{0}_t", GetAnimationTag()), _numberOfFrames, 134, true);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public ThrustAction() : base(8) {
        }

        public override string GetAnimationTag() {
            return "th";
        }

        public override string GetAnimationType() {
            return "thrust";
        }
    }
}