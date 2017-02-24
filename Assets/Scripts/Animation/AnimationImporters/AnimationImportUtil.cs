using Assets.Scripts.Character;
using Assets.Scripts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Animation.AnimationImporters {
    public class AnimationImportUtil {
        public AnimationDNABlock BuildAnimation(SingleAnimationImporter animationDefinition, string spritesheetKey, string direction) {
            string animationKey = spritesheetKey + "_" + animationDefinition.TagName;

            // fetch all frames for an model/action/direction into a list
            List<Sprite> spriteList = new List<Sprite>();
            for (int i = 0; i < animationDefinition.NumberOfFrames; i++) {
                string spriteKey = animationKey + "_" + i;
                Sprite sprite = AtlasManager.GetSprite(spriteKey);
                spriteList.Add(sprite);
            }

            // get the model key from the animation key
            string modelKey = animationKey.Split('_')[0].ToUpper();
            int sortingOrder = DNABlockType.GetSortingOrder(modelKey, direction);

            // create a new animation block
            return new AnimationDNABlock(animationKey, spriteList, direction, sortingOrder);
        }
    }
}
