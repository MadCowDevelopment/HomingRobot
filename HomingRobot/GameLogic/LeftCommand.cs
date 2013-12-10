namespace HomingRobot.GameLogic
{
    internal class LeftCommand : Command
    {
        public LeftCommand(Map map)
            : base(map)
        {
        }

        public override void Execute()
        {
            Map.MoveLeft();
        }
    }
}