using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class SpellcastAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter("sc_d", 7, 6, true);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter("sc_l", 7, 13, true);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter("sc_r", 7, 20, true);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter("sc_t", 7, 27, true);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }
    }
}
