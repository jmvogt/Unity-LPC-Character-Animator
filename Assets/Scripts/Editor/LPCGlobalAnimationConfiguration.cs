using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Editor {
    public class LPCGlobalAnimationConfiguration : ScriptableObject {
        [ShowInWindow(tab: "Import", readOnly: true)]
        public TextAsset CombinedDefinitions;

        [NonReorderable]
        [ShowInWindow(tab: "Import")]
        public List<TextAsset> Definitions = new();

        [NonReorderable]
        [ShowInWindow(tab: "Slot Types", readOnly: true)]
        public List<string> CharacterSlotTypes = new();

        [NonReorderable]
        [ShowInWindow(tab: "Slot Types", readOnly: true)]
        public List<LPCCharacterTypeConfiguration> Races = new();
    }
}
