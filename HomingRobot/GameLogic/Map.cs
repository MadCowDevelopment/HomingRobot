using System;
using System.Threading;

namespace HomingRobot.GameLogic
{
    internal class Map
    {
        private char[] _layout;
        private int _width;
        private int _height;

        public void Initialize(MapLayout mapLayout)
        {
            _layout = mapLayout.Layout;
            _width = mapLayout.Width;
            _height = mapLayout.Height;
        }

        public void MoveLeft()
        {
            Move(-1);
        }

        public void MoveRight()
        {
            Move(+1);
        }

        public void MoveUp()
        {
            Move(-_width);
        }

        public void MoveDown()
        {
            Move(_width);
        }

        private void Move(int offset)
        {
            var robotPos = FindRobot();
            var newPos = robotPos + offset;
            if (IsFreeSpace(newPos))
            {
                _layout[robotPos] = ' ';
                _layout[newPos] = '©';
                DrawMaze();
            }

            if (IsGoalLocation(newPos))
            {
                Console.WriteLine("YOU WIN");
                Console.WriteLine();
            }

            Thread.Sleep(200);
        }

        private bool IsGoalLocation(int index)
        {
            return _layout[index] == '@';
        }

        public void DrawMaze()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write(_layout[i * _width + j]);
                }

                Console.WriteLine();
            }
        }

        private int FindRobot()
        {
            for (int i = 0; i < _layout.Length; i++)
            {
                if (_layout[i] == '©') return i;
            }

            return -1;
        }

        private bool IsFreeSpace(int index)
        {
            return _layout[index] == ' ';
        }
    }
}