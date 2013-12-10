namespace HomingRobot.GameLogic
{
    internal abstract class Command
    {
        protected Map Map { get; private set; }

        protected Command(Map map)
        {
            Map = map;
        }

        public abstract void Execute();
    }
}