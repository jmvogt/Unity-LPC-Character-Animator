using Assets.Scripts.Animation.AnimationDirections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Animation.Interfaces
{
    public abstract class BaseAction
    {
        public abstract IAnimationImporter GetAnimationImporter();

        public abstract string GetAnimationTag();

        public BaseAnimationDirection Direction { get { return _direction; } set { _direction = value; } }

        protected int _numberOfFrames;
        protected BaseAnimationDirection _direction;
        public int NumberOfFrames { get { return _numberOfFrames;} }

        public BaseAction(int numberOfFrames) {
            _numberOfFrames = numberOfFrames;
            _direction = new NoAnimationDirection();
        }

        public abstract string GetAnimationType();

        public abstract bool GetStopOnLastFrame();
    }
}
