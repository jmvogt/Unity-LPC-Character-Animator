using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Types;

namespace Assets.Scripts.Animation
{
    public class AnimationDNA
    {
        public Dictionary<string, AnimationDNABlock> DNABlocks { get; } = DNABlockType.TypeList.ToDictionary(bt => bt, v => new AnimationDNABlock());
    }
}
