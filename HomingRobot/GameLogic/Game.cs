using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HomingRobot.GameLogic
{
    internal class Game
    {
        private readonly Map _map;

        private readonly int _maxCommandLines;
        private readonly int _maxCommandsPerLine;

        private readonly CommandContainer[] _container;

        public Game(int maxCommandLines, int maxCommandsPerLine, RandomCommandGenerator generator)
        {
            _maxCommandLines = maxCommandLines;
            _maxCommandsPerLine = maxCommandsPerLine;
            _container = new CommandContainer[_maxCommandLines];

            _map = new Map();
            _map.Initialize(new MapGenerator().CreateMap(generator.CreateCommands(_map).Commands));

            for (int i = 0; i < _maxCommandLines; i++)
            {
                _container[i] = new CommandContainer();
            }
        }

        public void Run()
        {
            _map.DrawMaze();

            for (int i = 0; i < _maxCommandLines; i++)
            {
                Console.Write("{0}: ", i + 1);
                var input = ReadUserInput();
                _container[i].Commands = ParseInput(input);
            }

            foreach (var command in _container[0].Commands)
            {
                command.Execute();
            }
        }

        private string ReadUserInput()
        {
            var pressedChars = new List<char>();
            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true);
                if (pressedChars.Count < _maxCommandsPerLine && PressedCharIsValid(pressedKey.KeyChar))
                {
                    pressedChars.Add(pressedKey.KeyChar);
                    Console.Write(pressedKey.KeyChar);
                }
                else if (pressedKey.Key == ConsoleKey.Backspace && pressedChars.Count > 0)
                {
                    pressedChars.RemoveAt(pressedChars.Count - 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
            } while (pressedKey.Key != ConsoleKey.Enter);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
            var input = new string(pressedChars.ToArray());
            return input;
        }

        private bool PressedCharIsValid(char keyChar)
        {
            if (keyChar == 'l' || keyChar == 'r' || keyChar == 'u' || keyChar == 'd') return true;
            int digit;
            if (int.TryParse(keyChar.ToString(CultureInfo.InvariantCulture), out digit))
            {
                return digit <= _maxCommandLines && digit > 0;
            }

            return false;
        }

        private IEnumerable<Command> ParseInput(string input)
        {
            var result = new List<Command>();
            foreach (var c in input.Take(_maxCommandsPerLine))
            {
                switch (c)
                {
                    case 'l':
                        result.Add(new LeftCommand(_map));
                        break;
                    case 'r':
                        result.Add(new RightCommand(_map));
                        break;
                    case 'u':
                        result.Add(new UpCommand(_map));
                        break;
                    case 'd':
                        result.Add(new DownCommand(_map));
                        break;
                }

                int digit;
                if (int.TryParse(c.ToString(CultureInfo.InvariantCulture), out digit))
                {
                    result.Add(new CompositeCommand(_container[digit], _map));
                }
            }

            return result;
        }
    }
}