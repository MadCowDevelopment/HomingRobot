using System.Collections.Generic;

namespace HomingRobot.Menus
{
    public class StartNewGameOption : MenuOption
    {
        public override string Text
        {
            get { return "Start new game"; }
        }

        public override void Execute(params object[] arguments)
        {
            new Menu("N E W   G A M E",
                new List<MenuOption>
                {
                    new StartOption(),
                    new MaxNumberOfCommandsOption(),
                    new MaxCommandsPerLineOption(),
                    new DifficultyOption()

                }).Show();
        }

        public override void Left()
        {
        }

        public override void Right()
        {
        }
    }
}