using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class ShootAction : BaseAction
    {
        public ShootAction()
        {
            NumberOfFrames = 13;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", NumberOfFrames, 34, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", NumberOfFrames, 47, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", NumberOfFrames, 60, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", NumberOfFrames, 73, GetStopOnLastFrame());
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
