using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.ActionAnimations
{
    internal class SpellcastAction : BaseAction
    {
        public SpellcastAction()
        {
            NumberOfFrames = 7;
        }

        public override IAnimationImporter GetAnimationImporter()
        {
            var downAnimation = new SingleAnimationImporter($"{AnimationTag}_d", NumberOfFrames, 6, StopOnLastFrame);
            var leftAnimation = new SingleAnimationImporter($"{AnimationTag}_l", NumberOfFrames, 13, StopOnLastFrame);
            var rightAnimation = new SingleAnimationImporter($"{AnimationTag}_r", NumberOfFrames, 20, StopOnLastFrame);
            var upAnimation = new SingleAnimationImporter($"{AnimationTag}_t", NumberOfFrames, 27, StopOnLastFrame);

            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public override string AnimationTag => "sc";
        public override string AnimationType => "spellcast";
        public override bool StopOnLastFrame => false;
    }
}
