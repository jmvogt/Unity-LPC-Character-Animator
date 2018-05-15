using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class ThrustAction : BaseAction
    {
        public ThrustAction()
        {
            _numberOfFrames = 8;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", _numberOfFrames, 110, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", _numberOfFrames, 118, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", _numberOfFrames, 126, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", _numberOfFrames, 134, GetStopOnLastFrame());

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string GetAnimationTag()
        {
            return "th";
        }

        public override string GetAnimationType()
        {
            return "thrust";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
