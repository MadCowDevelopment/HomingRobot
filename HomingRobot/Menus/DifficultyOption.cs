namespace HomingRobot.Menus
{
    public class DifficultyOption : MenuOption, IOptionWithValue
    {
        private Difficulty _difficulty;

        public override string Text
        {
            get { return string.Format("Difficulty: {0}", _difficulty); }
        }

        public override void Execute(params object[] arguments)
        {
        }

        public override void Left()
        {
            if ((int) _difficulty > 0) _difficulty--;
        }

        public override void Right()
        {
            if ((int)_difficulty < 4) _difficulty++;
        }

        public object Value { get { return _difficulty; } }
    }
}