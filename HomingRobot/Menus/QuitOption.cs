using System;

namespace HomingRobot.Menus
{
    public class QuitOption : MenuOption
    {
        public override string Text { get { return "Quit"; } }
        public override void Execute(params object[] arguments)
        {
            Environment.Exit(0);
        }

        public override void Left()
        {
        }

        public override void Right()
        {
        }
    }
}