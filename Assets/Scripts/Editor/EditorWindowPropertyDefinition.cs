namespace Assets.Scripts.Editor {
    public class EditorWindowPropertyDefinition {
        public readonly string PropertyName;
        public readonly bool ReadOnly;
        public readonly string Tab;
        public EditorWindowPropertyDefinition(string propertyName, ShowInWindowAttribute attribute) {
            PropertyName = propertyName;
            ReadOnly = attribute.ReadOnly;
            Tab = attribute.Tab;
        }
    }
}
