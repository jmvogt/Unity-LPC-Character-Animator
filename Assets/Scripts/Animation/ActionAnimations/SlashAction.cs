using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class SlashAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter("sl_d", 6, 86, false);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter("sl_l", 6, 92, false);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter("sl_r", 6, 98, false);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter("sl_t", 6, 104, false);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }
    }
}