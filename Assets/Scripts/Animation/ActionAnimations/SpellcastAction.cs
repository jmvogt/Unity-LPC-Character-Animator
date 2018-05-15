using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class SpellcastAction : BaseAction
    {
        public SpellcastAction()
        {
            _numberOfFrames = 7;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_d", _numberOfFrames, 6, GetStopOnLastFrame());
            var leftAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_l", _numberOfFrames, 13, GetStopOnLastFrame());
            var rightAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_r", _numberOfFrames, 20, GetStopOnLastFrame());
            var upAnimation = new SingleAnimationImporter($"{GetAnimationTag()}_t", _numberOfFrames, 27, GetStopOnLastFrame());

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string GetAnimationTag()
        {
            return "sc";
        }

        public override string GetAnimationType()
        {
            return "spellcast";
        }

        public override bool GetStopOnLastFrame()
        {
            return false;
        }
    }
}
