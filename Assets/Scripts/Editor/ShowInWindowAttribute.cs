using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ShowInWindowAttribute : Attribute {
        public readonly bool ReadOnly;
        public ShowInWindowAttribute(bool readOnly = false) {
            ReadOnly = readOnly;
        }
    }
}
