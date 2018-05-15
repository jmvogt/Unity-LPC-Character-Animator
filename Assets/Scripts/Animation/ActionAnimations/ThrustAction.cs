using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class ThrustAction : BaseAction
    {
        public ThrustAction()
        {
            NumberOfFrames = 8;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", NumberOfFrames, 110, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", NumberOfFrames, 118, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", NumberOfFrames, 126, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", NumberOfFrames, 134, GetStopOnLastFrame());

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
