namespace HomingRobot.GameLogic
{
    internal class RightCommand : Command
    {
        public RightCommand(Map map)
            : base(map)
        {
        }

        public override void Execute()
        {
            Map.MoveRight();
        }
    }
}