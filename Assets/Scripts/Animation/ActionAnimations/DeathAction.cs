using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class DeathAction : BaseAction
    {
        public DeathAction()
        {
            _numberOfFrames = 6;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var animationTag = GetAnimationTag();
            var spriteStartIndex = _numberOfFrames;
            var stopOnFinalFrame = GetStopOnLastFrame();
            return new SingleAnimationImporter(animationTag, _numberOfFrames, spriteStartIndex, stopOnFinalFrame);
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
