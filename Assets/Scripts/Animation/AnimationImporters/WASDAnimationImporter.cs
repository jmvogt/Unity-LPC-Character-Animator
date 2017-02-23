using Assets.Scripts.Animation.Interfaces;
using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.AnimationImporters
{
    public class WASDAnimationImporter : IAnimationImporter {
        private SingleAnimationImporter _w_importer;
        private SingleAnimationImporter _a_importer;
        private SingleAnimationImporter _s_importer;
        private SingleAnimationImporter _d_importer;

        List<AnimationDNABlock> IAnimationImporter.ImportAnimations(string spritesheetKey, string direction) {
            List<AnimationDNABlock> animationList = new List<AnimationDNABlock>();
            AnimationImportUtil builder = new AnimationImportUtil();
            animationList.Add(builder.BuildAnimation(_w_importer, spritesheetKey, DirectionType.UP));
            animationList.Add(builder.BuildAnimation(_a_importer, spritesheetKey, DirectionType.LEFT));
            animationList.Add(builder.BuildAnimation(_s_importer, spritesheetKey, DirectionType.DOWN));
            animationList.Add(builder.BuildAnimation(_d_importer, spritesheetKey, DirectionType.RIGHT));
            return animationList;
        }

        
        public WASDAnimationImporter(SingleAnimationImporter w, SingleAnimationImporter a, SingleAnimationImporter s, SingleAnimationImporter d) {
            _w_importer = w;
            _a_importer = a;
            _s_importer = s;
            _d_importer = d;
        }
    }
}
