using Assets.Scripts.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation
{
    public class AnimationDNA
    {
        public Animation BodyAnimation { get; set; }
        public Animation HairAnimation { get; set; }
        public Animation LegAnimation { get; set; }
        public Animation NeckAnimation { get; set; }
        public Animation BackAnimation { get; set; }
        public Animation Back2Animation { get; set; }
        public Animation WaistAnimation { get; set; }
        public Animation FaceAnimation { get; set; }
        public Animation FeetAnimation { get; set; }
        public Animation HandAnimation { get; set; }
        public Animation HeadAnimation { get; set; }
        public Animation TorsoAnimation { get; set; }
        public Animation PrimaryAnimation { get; set; }
        public Animation SecondaryAnimation { get; set; }

        public List<Animation> GetAnimatedDNA() {
            List<Animation> animatedList = new List<Animation>();
            if (BodyAnimation != null) animatedList.Add(BodyAnimation);
            if (HairAnimation != null) animatedList.Add(HairAnimation);
            if (LegAnimation != null) animatedList.Add(LegAnimation);
            if (NeckAnimation != null) animatedList.Add(NeckAnimation);
            if (BackAnimation != null) animatedList.Add(BackAnimation);
            if (Back2Animation != null) animatedList.Add(Back2Animation);
            if (WaistAnimation != null) animatedList.Add(WaistAnimation);
            if (FeetAnimation != null) animatedList.Add(FeetAnimation);
            if (HandAnimation != null) animatedList.Add(HandAnimation);
            if (HeadAnimation != null) animatedList.Add(HeadAnimation);
            if (TorsoAnimation != null) animatedList.Add(TorsoAnimation);
            if (PrimaryAnimation != null) animatedList.Add(PrimaryAnimation);
            if (SecondaryAnimation != null) animatedList.Add(SecondaryAnimation);
            return animatedList;
        }
    }
}