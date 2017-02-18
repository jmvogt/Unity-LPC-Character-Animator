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

        List<Animation> IAnimationImporter.ImportAnimations(string spritesheetKey) {
            List<Animation> animationList = new List<Animation>();
            AnimationBuilder builder = new AnimationBuilder();
            animationList.Add(builder.BuildAnimation(_w_importer, spritesheetKey));
            animationList.Add(builder.BuildAnimation(_a_importer, spritesheetKey));
            animationList.Add(builder.BuildAnimation(_s_importer, spritesheetKey));
            animationList.Add(builder.BuildAnimation(_d_importer, spritesheetKey));
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
