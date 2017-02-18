using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    public class ShootAction : IAction {
        public IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter("sh_d", 13, 34, false);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter("sh_l", 13, 47, false);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter("sh_r", 13, 60, false);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter("sh_t", 13, 73, false);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }
    }
}

