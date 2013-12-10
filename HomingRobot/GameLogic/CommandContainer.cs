using System.Collections.Generic;

namespace HomingRobot.GameLogic
{
    internal class CommandContainer
    {
        public IEnumerable<Command> Commands { get; set; }
    }
}