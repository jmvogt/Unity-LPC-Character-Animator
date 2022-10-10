namespace Assets.Scripts.Editor {
    public class EditorWindowPropertyDefinition {
        public readonly string PropertyName;
        public readonly bool ReadOnly;
        public readonly string PrimaryTab;
        public readonly string SecondaryTab;
        public EditorWindowPropertyDefinition(string propertyName, ShowInWindowAttribute attribute) {
            PropertyName = propertyName;
            ReadOnly = attribute.ReadOnly;
            PrimaryTab = attribute.PrimaryTab;
            SecondaryTab = attribute.SecondaryTab;
        }
    }
}
