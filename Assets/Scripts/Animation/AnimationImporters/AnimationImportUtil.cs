using System.Collections.Generic;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Animation.AnimationImporters
{
    public class AnimationImportUtil
    {
        public AnimationDNABlock BuildAnimation(SingleAnimationImporter animationDefinition, string spritesheetKey, string direction)
        {
            var animationKey = spritesheetKey + "_" + animationDefinition.TagName;

            // fetch all frames for an model/action/direction into a list
            var spriteList = new List<Sprite>();
            for (var i = 0; i < animationDefinition.NumberOfFrames; i++)
            {
                var spriteKey = animationKey + "_" + i;
                var sprite = AtlasManager.GetSprite(spriteKey);
                spriteList.Add(sprite);
            }

            // get the model key from the animation key
            var modelKey = animationKey.Split('_')[0].ToUpper();
            var sortingOrder = DNABlockType.GetSortingOrder(modelKey, direction);

            // create a new animation block
            return new AnimationDNABlock(animationKey, spriteList, direction, sortingOrder);
        }
    }
}
