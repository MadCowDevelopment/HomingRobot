using HomingRobot.GameLogic;

namespace HomingRobot.Menus
{
    public class StartOption : MenuOption
    {
        public override string Text
        {
            get { return "Start game"; }
        }

        public override void Execute(params object[] arguments)
        {
            var maxNumberOfCommandLines = (int) arguments[0];
            var maxCommandsPerLine = (int) arguments[1];
            var difficulty = (Difficulty) arguments[2];
            var generator = new RandomCommandGenerator(maxNumberOfCommandLines, maxCommandsPerLine, difficulty);
            new Game(maxNumberOfCommandLines, maxCommandsPerLine, generator).Run();
        }

        public override void Left()
        {
        }

        public override void Right()
        {
        }
    }
}