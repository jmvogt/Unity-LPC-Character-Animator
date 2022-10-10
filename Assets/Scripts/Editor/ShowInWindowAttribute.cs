using System;

namespace Assets.Scripts.Editor {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ShowInWindowAttribute : Attribute {
        public readonly bool ReadOnly;
        public readonly string PrimaryTab;
        public readonly string SecondaryTab;
        public ShowInWindowAttribute(bool readOnly = false, string primaryTab = "", string secondaryTab = "") {
            ReadOnly = readOnly;
            PrimaryTab = primaryTab;
            SecondaryTab = secondaryTab;
        }
    }
}
