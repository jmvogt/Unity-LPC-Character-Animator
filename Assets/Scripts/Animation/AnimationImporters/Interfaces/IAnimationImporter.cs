using System.Collections.Generic;

namespace Assets.Scripts.Animation.Interfaces
{
    public interface IAnimationImporter
    {
        List<AnimationDNABlock> ImportAnimations(string spritesheetKey, string direction);
    }
}
