using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class IdleAction : BaseAction
    {
        public IdleAction()
        {
            NumberOfFrames = 1;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var animationTag = AnimationTag;
            var spriteStartIndex = 0;
            var stopOnFinalFrame = StopOnLastFrame;
            return new SingleAnimationImporter(animationTag, NumberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public override string AnimationTag => "hu";
        public override string AnimationType => "idle";
        public override bool StopOnLastFrame => false;
    }
}
