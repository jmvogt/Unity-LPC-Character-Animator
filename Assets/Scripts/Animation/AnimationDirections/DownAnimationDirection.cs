using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.AnimationDirections {
    public class DownAnimationDirection: IAnimationDirection {
        public char GetAnimationDirection() {
            return 'd';
        }
    }
}
