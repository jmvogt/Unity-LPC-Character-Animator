using System.Collections.Generic;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class AnimationDNABlock
    {
        public AnimationDNABlock(string animationKey, List<Sprite> spriteList, string direction, int sortingOrder)
        {
            AnimationKey = animationKey;
            SpriteList = spriteList;
            Direction = direction;
            SortingOrder = sortingOrder;
            Enabled = true;
            IsDirty = true;
        }

        public AnimationDNABlock()
        {
            AnimationKey = "UNKNOWN";
            Direction = DirectionType.None;
            Enabled = false;
            SortingOrder = -99;
        }

        public string AnimationKey { get; }
        public List<Sprite> SpriteList { get; }
        public int SortingOrder { get; }
        public string Direction { get; }
        public bool Enabled { get; set; }
        public bool IsDirty { get; set; }
        public Color SpriteColor { get; private set; }

        public void UpdateSpriteColor(Color spriteColor)
        {
            SpriteColor = spriteColor;
            IsDirty = true;
        }
    }
}
