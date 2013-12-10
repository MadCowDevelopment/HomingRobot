using System.Collections.Generic;
using HomingRobot.Menus;

namespace HomingRobot
{
    class Program
    {
        static void Main()
        {
            new Menu("M A I N   M E N U", new List<MenuOption> {new StartNewGameOption(), new QuitOption()}).Show();
        }
    }
}
