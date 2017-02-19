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

        public IAnimationDirection Direction { get; set; }

        protected int _numberOfFrames;
        public int NumberOfFrames { get { return _numberOfFrames;} }

        public BaseAction(int numberOfFrames) {
            _numberOfFrames = numberOfFrames;
        }

        public abstract string GetAnimationType();
    }
}
