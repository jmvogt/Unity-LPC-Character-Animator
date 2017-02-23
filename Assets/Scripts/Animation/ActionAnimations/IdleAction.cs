using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class IdleAction : BaseAction {
        public override IAnimationImporter GetAnimationImporter() {
            string animationTag = GetAnimationTag();
            int spriteStartIndex = 0;
            bool stopOnFinalFrame = GetStopOnLastFrame();
            return new SingleAnimationImporter(animationTag, _numberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public IdleAction() : base() {
            _numberOfFrames = 1;
        }

        public override string GetAnimationTag() {
            return "hu";
        }

        public override string GetAnimationType() {
            return "idle";
        }

        public override bool GetStopOnLastFrame() {
            return false;
        }
    }
}
