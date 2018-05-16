using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class WalkAction : BaseAction
    {
        public WalkAction()
        {
            NumberOfFrames = 9;
            FramesPerSecond = 2;
        }

        public override string AnimationTag => "wc";
        public override string AnimationType => "walk";
        public override bool StopOnLastFrame => false;

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{AnimationTag}_d", NumberOfFrames, 142, StopOnLastFrame);
            var leftAnimation = new SingleAnimationImporter($"{AnimationTag}_l", NumberOfFrames, 151, StopOnLastFrame);
            var rightAnimation = new SingleAnimationImporter($"{AnimationTag}_r", NumberOfFrames, 160, StopOnLastFrame);
            var upAnimation = new SingleAnimationImporter($"{AnimationTag}_t", NumberOfFrames, 169, StopOnLastFrame);

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }
    }
}
