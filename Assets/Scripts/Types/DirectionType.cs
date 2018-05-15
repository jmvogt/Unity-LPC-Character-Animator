namespace Assets.Scripts.Types
{
    public static class DirectionType
    {
        public const string None = "NONE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Up = "UP";
        public const string Down = "DOWN";

        public static string GetAnimationForDirection(string direction)
        {
            switch (direction)
            {
                case Left:
                    return "l";
                case Right:
                    return "r";
                case Up:
                    return "t";
                case Down:
                    return "d";
                default:
                    return "";
            }
        }
    }
}
