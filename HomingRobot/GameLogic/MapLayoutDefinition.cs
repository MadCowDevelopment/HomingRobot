namespace HomingRobot.GameLogic
{
    internal class MapLayoutDefinition
    {
        public int Width
        {
            get { return MaxX - MinX + 1 + 2; }
        }

        public int Height
        {
            get { return MaxY - MinY + 1 + 2; }
        }

        public Point StartingPos
        {
            get
            {
                int x = 1;
                if (MinX < 0)
                {
                    x = 0 - MinX + 1;
                }

                int y = 1;
                if (MinY < 0)
                {
                    y = 0 - MinY + 1;
                }

                return new Point(x, y);
            }
        }

        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
    }
}