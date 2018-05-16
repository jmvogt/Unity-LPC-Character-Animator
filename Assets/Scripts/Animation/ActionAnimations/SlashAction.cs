using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class SlashAction : BaseAction
    {
        public SlashAction()
        {
            NumberOfFrames = 6;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{AnimationTag}_d", NumberOfFrames, 86, StopOnLastFrame);
            var leftAnimation = new SingleAnimationImporter($"{AnimationTag}_l", NumberOfFrames, 92, StopOnLastFrame);
            var rightAnimation = new SingleAnimationImporter($"{AnimationTag}_r", NumberOfFrames, 98, StopOnLastFrame);
            var upAnimation = new SingleAnimationImporter($"{AnimationTag}_t", NumberOfFrames, 104, StopOnLastFrame);

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string AnimationTag => "sl";
        public override string AnimationType => "slash";
        public override bool StopOnLastFrame => false;
    }
}
