namespace Assets.Scripts.Animation.Interfaces
{
    public abstract class BaseAction
    {
        public string Direction { get; set; }

        public int NumberOfFrames { get; protected set; }
        public int FramesPerSecond { get; protected set; }

        public abstract IAnimationImporter GetAnimationImporter();

        public abstract string GetAnimationTag();

        public abstract string GetAnimationType();

        public abstract bool GetStopOnLastFrame();
    }
}
