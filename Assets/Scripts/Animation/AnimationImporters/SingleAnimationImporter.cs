using System.Collections.Generic;
using Assets.Scripts.Animation.Interfaces;

namespace Assets.Scripts.Animation.AnimationImporters
{
    public class SingleAnimationImporter : IAnimationImporter
    {
        public SingleAnimationImporter(string tagName, int numberOfFrames, int spriteStartIndex, bool stopOnFinalFrame)
        {
            TagName = tagName;
            NumberOfFrames = numberOfFrames;
            SpriteStartIndex = spriteStartIndex;
            StopOnFinalFrame = stopOnFinalFrame;
        }

        List<AnimationDNABlock> IAnimationImporter.ImportAnimations(string spritesheetKey, string direction)
        {
            var animationList = new List<AnimationDNABlock>();
            var builder = new AnimationImportUtil();
            animationList.Add(builder.BuildAnimation(this, spritesheetKey, direction));
            return animationList;
        }

        public string TagName { get; }
        public int NumberOfFrames { get; }
        public int SpriteStartIndex { get; }
        public bool StopOnFinalFrame { get; }
    }
}
