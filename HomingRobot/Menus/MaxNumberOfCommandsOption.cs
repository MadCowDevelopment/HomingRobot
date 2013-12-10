namespace HomingRobot.Menus
{
    public class MaxNumberOfCommandsOption : MenuOption, IOptionWithValue
    {
        private int _numberOfCommandLines = 4;

        public override string Text
        {
            get { return string.Format("Max number of command lines: {0}", _numberOfCommandLines); }
        }

        public override void Execute(params object[] arguments)
        {
        }

        public override void Left()
        {
            if (_numberOfCommandLines > 2) _numberOfCommandLines--;
        }

        public override void Right()
        {
            if (_numberOfCommandLines < 8) _numberOfCommandLines++;
        }

        public object Value { get { return _numberOfCommandLines; } }
    }
}