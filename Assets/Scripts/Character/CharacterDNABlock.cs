using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterDNABlock
    {
        public CharacterDNABlock()
        {
            ModelKey = "UNKNOWN";
            ItemColor = new Color();
            Enabled = false;
            IsDirty = false;
        }

        public CharacterDNABlock(string itemKey)
        {
            Update(itemKey, new Color());
        }

        public CharacterDNABlock(string itemKey, Color itemColor)
        {
            Update(itemKey, itemColor);
        }

        public string ModelKey { get; set; }

        public Color ItemColor { get; set; }

        public bool Enabled { get; set; }

        public bool IsDirty { get; set; }

        public void Update(string itemKey, Color itemColor)
        {
            ModelKey = itemKey;
            ItemColor = itemColor;
            Enabled = true;
            IsDirty = true;

            // disable the character block if there is no model key
            if (itemKey.Length > 0)
                Enabled = true;
            else
                Enabled = false;
        }

        public void UpdateColor(Color itemColor)
        {
            ItemColor = itemColor;
            Enabled = true;
            IsDirty = true;
        }

        public void UpdateModel(string modelKey)
        {
            ModelKey = modelKey;
            IsDirty = true;

            // disable the character block if there is no model key
            if (modelKey.Length > 0)
                Enabled = true;
            else
                Enabled = false;
        }
    }
}
