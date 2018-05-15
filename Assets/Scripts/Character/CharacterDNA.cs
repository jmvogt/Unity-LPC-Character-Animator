using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterDNA
    {
        // This is the LPC Character DNA Block class! In a real use case, 
        // this information would be represented by both the character's 
        // stats (race, hair, etc..) as well as their armor/weapons rather 
        // than being lumped together into one class

        public Dictionary<string, CharacterDNABlock> DNABlocks { get; } = DNABlockType.TypeList.ToDictionary(bt => bt, v => new CharacterDNABlock());

        public void UpdateBlock(string blockKey, string itemKey, Color color)
        {
            var dnaBlock = DNABlocks[blockKey];
            dnaBlock.Update(itemKey, color);
        }

        public void UpdateBlockColor(string blockKey, Color color)
        {
            var dnaBlock = DNABlocks[blockKey];
            dnaBlock.UpdateColor(color);
        }

        public void UpdateBlockModel(string blockKey, string modelKey)
        {
            var dnaBlock = DNABlocks[blockKey];
            dnaBlock.UpdateModel(modelKey);
        }

        public bool IsDirty()
        {
            foreach (var blockKey in DNABlocks.Keys)
            {
                if (DNABlocks[blockKey].IsDirty) return true;
            }

            return false;
        }
    }
}
