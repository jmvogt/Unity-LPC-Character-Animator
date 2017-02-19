using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.AnimationDirections {
    public class RightAnimationDirection : BaseAnimationDirection {
        public override string GetAnimationDirection() {
            return "r";
        }
    }
}
