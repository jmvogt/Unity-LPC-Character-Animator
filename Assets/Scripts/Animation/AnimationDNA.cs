using Assets.Scripts.Character;
using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    public class AnimationDNA
    {
        private Dictionary<string, AnimationDNABlock> _dnaBlocks;

        public Dictionary<string, AnimationDNABlock> DNABlocks { get { return _dnaBlocks; } set { _dnaBlocks = value; } }

        public AnimationDNA() {
            /*
             *  Initialize a block for all DNA Types
             */

            _dnaBlocks = new Dictionary<string, AnimationDNABlock>();
            foreach (string blockKey in DNABlockType.GetTypeList()) {
                _dnaBlocks.Add(blockKey, new AnimationDNABlock());
            }
        }
    }
}