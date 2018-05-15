using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class ShootAction : BaseAction
    {
        public ShootAction()
        {
            _numberOfFrames = 13;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", _numberOfFrames, 34, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", _numberOfFrames, 47, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", _numberOfFrames, 60, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", _numberOfFrames, 73, GetStopOnLastFrame());
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string GetAnimationTag()
        {
            return "sh";
        }

        public override string GetAnimationType()
        {
            return "shoot";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
