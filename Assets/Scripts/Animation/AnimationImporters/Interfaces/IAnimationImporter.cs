using Assets.Scripts.Animation.AnimationDirections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation.Interfaces
{
    public interface IAnimationImporter
    {
        List<AnimationDNABlock> ImportAnimations(string spritesheetKey, BaseAnimationDirection direction);

    }
}
