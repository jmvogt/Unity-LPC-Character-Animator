using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterDNA
    {
        /*
        * This is the LPC Character DNA Block class! In a real use case, 
        * this information would be represented by both the character's 
        * stats (race, hair, etc..) as well as their armor/weapons rather 
        * than being lumped together into one class
        */

        private Dictionary<string, CharacterDNABlock> _dnaBlocks;

        public Dictionary<string, CharacterDNABlock> DNABlocks { get { return _dnaBlocks; } set { _dnaBlocks = value; } }


        public CharacterDNA() {
            _dnaBlocks = new Dictionary<string, CharacterDNABlock>();
            foreach (string blockKey in DNABlockType.GetTypeList()) {
                _dnaBlocks.Add(blockKey, new CharacterDNABlock());
            }
        }

        public void UpdateBlock(string blockKey, string itemKey, Color color) {
            CharacterDNABlock dnaBlock = _dnaBlocks[blockKey];
            dnaBlock.Update(itemKey, color);
        }

        public void UpdateBlockColor(string blockKey, Color color) {
            CharacterDNABlock dnaBlock = _dnaBlocks[blockKey];
            dnaBlock.UpdateColor(color);
        }


        public void UpdateBlockModel(string blockKey, string modelKey) {
            CharacterDNABlock dnaBlock = _dnaBlocks[blockKey];
            dnaBlock.UpdateModel(modelKey);
        }

        public bool IsDirty() {
            foreach (string blockKey in _dnaBlocks.Keys) {
                if (_dnaBlocks[blockKey].IsDirty) { 
                    return true;
                }
            }
            return false;
        }
    }
}
