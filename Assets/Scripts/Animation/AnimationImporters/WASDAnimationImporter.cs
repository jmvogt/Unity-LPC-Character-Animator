using Assets.Scripts.Animation.AnimationDirections;
using Assets.Scripts.Animation.DNABlocks;
using Assets.Scripts.Animation.Interfaces;
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

        List<BaseAnimationDNABlock> IAnimationImporter.ImportAnimations(string spritesheetKey, BaseAnimationDirection direction) {
            List<BaseAnimationDNABlock> animationList = new List<BaseAnimationDNABlock>();
            AnimationBuilder builder = new AnimationBuilder();
            animationList.Add(builder.BuildAnimation(_w_importer, spritesheetKey, new UpAnimationDirection()));
            animationList.Add(builder.BuildAnimation(_a_importer, spritesheetKey, new LeftAnimationDirection()));
            animationList.Add(builder.BuildAnimation(_s_importer, spritesheetKey, new DownAnimationDirection()));
            animationList.Add(builder.BuildAnimation(_d_importer, spritesheetKey, new RightAnimationDirection()));
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
