using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class SlashAction : BaseAction
    {
        public SlashAction()
        {
            _numberOfFrames = 6;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", _numberOfFrames, 86, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", _numberOfFrames, 92, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", _numberOfFrames, 98, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", _numberOfFrames, 104, GetStopOnLastFrame());

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string GetAnimationTag()
        {
            return "sl";
        }

        public override string GetAnimationType()
        {
            return "slash";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
