using System.Collections.Generic;
using Assets.Scripts.Types;

namespace Assets.Scripts.Animation
{
    public class AnimationDNA
    {
        public AnimationDNA()
        {
            // Initialize a block for all DNA Types
            DNABlocks = new Dictionary<string, AnimationDNABlock>();
            foreach (var blockKey in DNABlockType.TypeList)
            {
                DNABlocks.Add(blockKey, new AnimationDNABlock());
            }
        }

        public Dictionary<string, AnimationDNABlock> DNABlocks { get; }
    }
}
