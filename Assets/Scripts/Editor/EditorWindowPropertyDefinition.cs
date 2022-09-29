using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor {
    public class EditorWindowPropertyDefinition {
        public readonly string PropertyName;
        public readonly bool ReadOnly;
        public EditorWindowPropertyDefinition(string propertyName, ShowInWindowAttribute attribute) {
            PropertyName = propertyName;
            ReadOnly = attribute.ReadOnly;
        }
    }
}
