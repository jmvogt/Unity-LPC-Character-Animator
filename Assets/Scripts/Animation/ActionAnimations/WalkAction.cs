using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class WalkAction : BaseAction
    {
        public WalkAction()
        {
            _numberOfFrames = 9;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", _numberOfFrames, 142, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", _numberOfFrames, 151, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", _numberOfFrames, 160, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", _numberOfFrames, 169, GetStopOnLastFrame());

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string GetAnimationTag()
        {
            return "wc";
        }

        public override string GetAnimationType()
        {
            return "walk";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
