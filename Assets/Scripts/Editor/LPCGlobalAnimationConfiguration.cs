using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor {
    public class LPCGlobalAnimationConfiguration : ScriptableObject {
        [NonReorderable]
        [ShowInWindow]
        public List<TextAsset> Definitions = new();

        [NonReorderable]
        [ShowInWindow(readOnly: true)]
        public List<string> CharacterSlotTypes = new();


        [NonReorderable]
        [ShowInWindow(readOnly: true)]
        public List<LPCCharacterTypeConfiguration> Races = new();
    }
}
