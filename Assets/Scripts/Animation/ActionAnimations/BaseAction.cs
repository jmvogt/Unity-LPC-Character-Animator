namespace Assets.Scripts.Animation.Interfaces
{
    public abstract class BaseAction
    {
        public string Direction { get; set; }

        public int NumberOfFrames { get; protected set; }
        public int FramesPerSecond { get; protected set; }

        public abstract string AnimationTag { get; }
        public abstract string AnimationType { get; }
        public abstract bool StopOnLastFrame { get; }

        public abstract IAnimationImporter GetAnimationImporter();
    }
}
