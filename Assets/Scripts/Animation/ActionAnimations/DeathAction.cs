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
            var animationTag = GetAnimationTag();
            var spriteStartIndex = NumberOfFrames;
            var stopOnFinalFrame = GetStopOnLastFrame();
            return new SingleAnimationImporter(animationTag, NumberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public override string GetAnimationTag()
        {
            return "hu";
        }

        public override string GetAnimationType()
        {
            return "death";
        }

        public override bool GetStopOnLastFrame()
        {
            return true;
        }
    }
}
