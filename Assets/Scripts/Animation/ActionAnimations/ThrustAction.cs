using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class ThrustAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter("th_d", 8, 110, true);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter("th_l", 8, 118, true);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter("th_r", 8, 126, true);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter("th_t", 8, 134, true);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }
    }
}