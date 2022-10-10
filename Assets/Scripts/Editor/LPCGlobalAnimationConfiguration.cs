using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Editor {
    public class LPCGlobalAnimationConfiguration : ScriptableObject {
        [ShowInWindow(primaryTab: "Import", secondaryTab: "LPC Unity Assets", readOnly: true)]
        public TextAsset CombinedDefinitions;

        [NonReorderable]
        [ShowInWindow(primaryTab: "Import", secondaryTab: "LPC Spritesheet")]
        public List<TextAsset> Definitions = new();

        [NonReorderable]
        [ShowInWindow(primaryTab: "Slot Types", readOnly: true)]
        public List<string> CharacterSlotTypes = new();

        // TODO: Paginate
        //[NonReorderable]
        //[ShowInWindow(primaryTab: "Slot Types", readOnly: true)]
        //public List<LPCCharacterTypeConfiguration> Races = new();
    }
}
