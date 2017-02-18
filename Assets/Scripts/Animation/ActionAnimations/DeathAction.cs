using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class DeathAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            string animationTag = "hu";
            int numberOfFrames = 6;
            int spriteStartIndex = 0;
            bool stopOnFinalFrame = true;
            return new SingleAnimationImporter(animationTag, numberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }
    }
}
