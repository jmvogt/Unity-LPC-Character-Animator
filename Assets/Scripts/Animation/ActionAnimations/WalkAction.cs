﻿using Assets.Scripts.Animation.AnimationImporters;
using Assets.Scripts.Animation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.ActionAnimations
{
    class WalkAction : BaseAction
    {
        public override IAnimationImporter GetAnimationImporter() {
            SingleAnimationImporter downAnimation = new SingleAnimationImporter(
                String.Format("{0}_d", GetAnimationTag()), _numberOfFrames, 142, false);
            SingleAnimationImporter leftAnimation = new SingleAnimationImporter(
                String.Format("{0}_l", GetAnimationTag()), _numberOfFrames, 151, false);
            SingleAnimationImporter rightAnimation = new SingleAnimationImporter(
                String.Format("{0}_r", GetAnimationTag()), _numberOfFrames, 160, false);
            SingleAnimationImporter upAnimation = new SingleAnimationImporter(
                String.Format("{0}_t", GetAnimationTag()), _numberOfFrames, 169, false);
            return new WASDAnimationImporter(upAnimation, leftAnimation, downAnimation, rightAnimation);
        }

        public WalkAction() : base(9) {
        }

        public override string GetAnimationTag() {
            return "wc";
        }

        public override string GetAnimationType() {
            return "walk";
        }

    }
}