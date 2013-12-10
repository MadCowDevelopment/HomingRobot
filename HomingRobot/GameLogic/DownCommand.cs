namespace HomingRobot.GameLogic
{
    internal class DownCommand : Command
    {
        public DownCommand(Map map)
            : base(map)
        {
        }

        public override void Execute()
        {
            Map.MoveDown();
        }
    }
}