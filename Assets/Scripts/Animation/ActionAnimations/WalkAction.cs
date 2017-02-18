using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class WalkAction : IAction
    {
        public IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter("wc_d", 9, 142, false);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter("wc_l", 9, 151, false);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter("wc_r", 9, 160, false);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter("wc_t", 9, 169, false);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }


    }
}
