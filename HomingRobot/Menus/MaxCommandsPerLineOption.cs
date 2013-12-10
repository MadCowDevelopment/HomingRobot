namespace HomingRobot.Menus
{
    public class MaxCommandsPerLineOption : MenuOption, IOptionWithValue
    {
        private int _commandsPerLine = 6;

        public override string Text
        {
            get { return string.Format("Max commands per line: {0}", _commandsPerLine); }
        }

        public override void Execute(params object[] arguments)
        {
        }

        public override void Left()
        {
            if (_commandsPerLine > 3) _commandsPerLine--;
        }

        public override void Right()
        {
            if (_commandsPerLine < 8) _commandsPerLine++;
        }

        public object Value { get { return _commandsPerLine; } }
    }
}