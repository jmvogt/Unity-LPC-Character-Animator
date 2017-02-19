using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class DeathAction : BaseAction {
        public override IAnimationImporter GetAnimationImporter() {
            string animationTag = GetAnimationTag();
            int spriteStartIndex = _numberOfFrames;
            bool stopOnFinalFrame = GetStopOnLastFrame();
            return new SingleAnimationImporter(animationTag, _numberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public DeathAction() : base(6) {
        }

        public override string GetAnimationTag() {
            return "hu";
        }

        public override string GetAnimationType() {
            return "death";
        }

        public override bool GetStopOnLastFrame() {
            return true;
        }
    }
}
