using System.Collections.Generic;
using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Types;

namespace Assets.Scripts.Animation.AnimationImporters
{
    public class WASDAnimationImporter : IAnimationImporter
    {
        private readonly SingleAnimationImporter _a_importer;
        private readonly SingleAnimationImporter _d_importer;
        private readonly SingleAnimationImporter _s_importer;
        private readonly SingleAnimationImporter _w_importer;


        public WASDAnimationImporter(SingleAnimationImporter w, SingleAnimationImporter a, SingleAnimationImporter s, SingleAnimationImporter d)
        {
            _w_importer = w;
            _a_importer = a;
            _s_importer = s;
            _d_importer = d;
        }

        List<AnimationDNABlock> IAnimationImporter.ImportAnimations(string spritesheetKey, string direction)
        {
            //  Builds all directional animations for a spritesheet

            var animationList = new List<AnimationDNABlock>();
            var builder = new AnimationImportUtil();
            animationList.Add(builder.BuildAnimation(_w_importer, spritesheetKey, DirectionType.Up));
            animationList.Add(builder.BuildAnimation(_a_importer, spritesheetKey, DirectionType.Left));
            animationList.Add(builder.BuildAnimation(_s_importer, spritesheetKey, DirectionType.Down));
            animationList.Add(builder.BuildAnimation(_d_importer, spritesheetKey, DirectionType.Right));
            return animationList;
        }
    }
}
