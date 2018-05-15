namespace Assets.Scripts.Animation.Interfaces
{
    public abstract class BaseAction
    {
        protected string _direction;

        protected int _numberOfFrames;

        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int NumberOfFrames => _numberOfFrames;
        public abstract IAnimationImporter GetAnimationImporter();

        public abstract string GetAnimationTag();

        public abstract string GetAnimationType();

        public abstract bool GetStopOnLastFrame();
    }
}
