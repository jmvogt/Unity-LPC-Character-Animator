using System;

namespace Assets.Scripts.Animation.Interfaces
{
    public abstract class BaseAction
    {
        private int _numberOfFrames;
        public string Direction { get; set; }

        public int NumberOfFrames
        {
            get { return _numberOfFrames; }
            protected set
            {

                _numberOfFrames = Math.Max(value, 1);
            }
        }

        public abstract string AnimationTag { get; }
        public abstract string AnimationType { get; }
        public abstract bool StopOnLastFrame { get; }

        public abstract IAnimationImporter GetAnimationImporter();
    }
}
