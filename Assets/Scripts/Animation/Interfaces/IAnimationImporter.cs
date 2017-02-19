using Assets.Scripts.Animation.DNABlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation.Interfaces
{
    public interface IAnimationImporter
    {
        List<BaseAnimationDNABlock> ImportAnimations(string spritesheetKey, IAnimationDirection direction);

    }
}
