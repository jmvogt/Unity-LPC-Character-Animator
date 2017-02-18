using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class IdleAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            string animationTag = "hu";
            int numberOfFrames = 1;
            int spriteStartIndex = 0;
            bool stopOnFinalFrame = false;
            return new SingleAnimationImporter(animationTag, numberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }
    }
}
