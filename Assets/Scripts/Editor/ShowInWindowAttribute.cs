using System;

namespace Assets.Scripts.Editor {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ShowInWindowAttribute : Attribute {
        public readonly bool ReadOnly;
        public readonly string Tab;
        public ShowInWindowAttribute(bool readOnly = false, string tab = "") {
            ReadOnly = readOnly;
            Tab = tab;
        }
    }
}
