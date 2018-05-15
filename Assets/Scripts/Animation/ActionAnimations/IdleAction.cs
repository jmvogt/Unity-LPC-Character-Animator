using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class IdleAction : BaseAction
    {
        public IdleAction()
        {
            _numberOfFrames = 1;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var animationTag = GetAnimationTag();
            var spriteStartIndex = 0;
            var stopOnFinalFrame = GetStopOnLastFrame();
            return new SingleAnimationImporter(animationTag, _numberOfFrames, spriteStartIndex, stopOnFinalFrame);
        }

        public override string GetAnimationTag()
        {
            return "hu";
        }

        public override string GetAnimationType()
        {
            return "idle";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
