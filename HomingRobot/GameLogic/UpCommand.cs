namespace HomingRobot.GameLogic
{
    internal class UpCommand : Command
    {
        public UpCommand(Map map)
            : base(map)
        {
        }

        public override void Execute()
        {
            Map.MoveUp();
        }
    }
}