using System;
using System.Collections.Generic;
using System.Linq;
using HomingRobot.Utils;

namespace HomingRobot.Menus
{
    public class Menu
    {
        private readonly string _title;
        private readonly List<MenuOption> _options;

        private MenuOption _selectedOption;

        public Menu(string title, IEnumerable<MenuOption> options)
        {
            _title = title;
            _options = options.ToList();
            ShouldShow = true;

            _selectedOption = _options[0];
        }
        public void Show()
        {
            do
            {
                PrintMenu();
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        _selectedOption = _options[(_options.IndexOf(_selectedOption) + 1)%(_options.Count)];
                        break;
                    case ConsoleKey.UpArrow:
                        var index = Math.Abs((_options.IndexOf(_selectedOption) - 1)%(_options.Count));
                        _selectedOption = _options[index];
                        break;
                    case ConsoleKey.LeftArrow:
                        _selectedOption.Left();
                        break;
                    case ConsoleKey.RightArrow:
                        _selectedOption.Right();
                        break;
                    case ConsoleKey.Enter:
                        _selectedOption.Execute(Arguments);
                        break;
                    case ConsoleKey.Escape:
                        ShouldShow = false;
                        break;
                }
            } while (ShouldShow);
        }

        public object[] Arguments
        {
            get
            {
                return _options.OfType<IOptionWithValue>().Select(p => p.Value).ToArray();
            }
        }

        private bool ShouldShow { get; set; }

        private void PrintMenu()
        {
            var previousForeground = Console.ForegroundColor;
            var previousBackground = Console.BackgroundColor;

            try
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ConsoleUtils.WriteLineCentered(string.Format("┌{0}┐", "─".Repeat(_title.Length + 2)));
                ConsoleUtils.WriteLineCentered(string.Format("│ {0} │", _title));
                ConsoleUtils.WriteLineCentered(string.Format("└{0}┘", "─".Repeat(_title.Length + 2)));

                Console.WriteLine();

                foreach (var menuOption in _options)
                {
                    if (menuOption == _selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    ConsoleUtils.WriteLineCentered(menuOption.Text);
                }
            }
            finally
            {
                Console.ForegroundColor = previousForeground;
                Console.BackgroundColor = previousBackground;
            }
        }

    }

    public interface IOptionWithValue
    {
        object Value { get; }
    }
}
