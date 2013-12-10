using System;
using System.Collections.Generic;
using HomingRobot.Menus;

namespace HomingRobot.GameLogic
{
    internal class RandomCommandGenerator
    {
        private readonly int _maxNumberOfCommandLines;
        private readonly int _maxNumberOfCommandsPerLine;
        private readonly Difficulty _difficulty;

        private Random _random = new Random((int) DateTime.Now.Ticks);

        public RandomCommandGenerator(int maxNumberOfCommandLines, int maxNumberOfCommandsPerLine, Difficulty difficulty)
        {
            _maxNumberOfCommandLines = maxNumberOfCommandLines;
            _maxNumberOfCommandsPerLine = maxNumberOfCommandsPerLine;
            _difficulty = difficulty;
        }

        public CommandContainer CreateCommands(Map map)
        {
            var container = new List<CommandContainer>();
            for (int i = 0; i < _maxNumberOfCommandLines; i++)
            {
                container.Add(new CommandContainer());
            }

            var commands = new List<Command>();
            for (int i = 0; i < _maxNumberOfCommandsPerLine; i++)
            {
                commands.Add(new CompositeCommand(container[_random.Next(1, _maxNumberOfCommandLines)], map));
            }
            container[0].Commands = commands;

            for (int i = 1; i < _maxNumberOfCommandLines; i++)
            {
                commands = new List<Command>();
                for (int j = 0; j < _maxNumberOfCommandsPerLine; j++)
                {
                    commands.Add(CreateRandomCommand(map));
                }
                container[i].Commands = commands;
            }

            return container[0];
        }

        private Command _lastCommand;

        private Command CreateRandomCommand(Map map)
        {
            Command command = null;
            do
            {
                var random = _random.Next(0, 3);
                switch (random)
                {
                    case 0:
                        command = new LeftCommand(map);
                        break;
                    case 1:
                        command = new RightCommand(map);
                        break;
                    case 2:
                        command = new UpCommand(map);
                        break;
                    case 3:
                        command = new DownCommand(map);
                        break;
                }
            } while (_lastCommand != null && _lastCommand.GetType() == command.GetType() );

            _lastCommand = command;
            return command;
        }
    }
}
