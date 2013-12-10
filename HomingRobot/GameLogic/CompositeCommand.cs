namespace HomingRobot.GameLogic
{
    internal class CompositeCommand : Command
    {
        internal readonly CommandContainer _container;

        public CompositeCommand(CommandContainer container, Map map)
            : base(map)
        {
            _container = container;
        }

        public override void Execute()
        {
            foreach (var command in _container.Commands)
            {
                command.Execute();
            }
        }
    }
}