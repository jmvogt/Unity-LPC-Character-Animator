using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class DeathAction : BaseAction
    {
        public DeathAction()
        {
            NumberOfFrames = 6;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var animationTag = AnimationTag;
            var spriteStartIndex = NumberOfFrames;
            var stopOnFinalFrame = StopOnLastFrame;
            return new SingleAnimationImporter(animationTag, NumberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public override string AnimationTag => "hu";
        public override string AnimationType => "death";
        public override bool StopOnLastFrame => true;
    }
}
