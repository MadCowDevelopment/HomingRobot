using System.Collections.Generic;

namespace HomingRobot.GameLogic
{
    internal class MapLayout
    {
        private readonly MapLayoutDefinition _def;
        private readonly IEnumerable<Command> _commands;
        internal char[] Layout { get; private set; }

        internal int Width { get { return _def.Width; } }

        internal int Height { get { return _def.Height; } }

        public MapLayout(MapLayoutDefinition def, IEnumerable<Command> commands)
        {
            _def = def;
            _commands = commands;
            Layout = new char[def.Width * def.Height];
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            SetPathTiles();
            SetBlockingTiles();
        }

        private void SetPathTiles()
        {
            _currentPosition = new Point(_def.StartingPos.X, _def.StartingPos.Y);
            FollowPath(_commands);
            SetTile(_def.StartingPos.X, _def.StartingPos.Y, '©');
            SetTile('@');
        }

        private Point _currentPosition;

        private void FollowPath(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                if (command is UpCommand)
                {
                    _currentPosition.Y--;
                    SetTile(' ');
                }
                else if (command is DownCommand)
                {
                    _currentPosition.Y++;
                    SetTile(' ');
                }
                else if (command is LeftCommand)
                {
                    _currentPosition.X--;
                    SetTile(' ');
                }
                else if (command is RightCommand)
                {
                    _currentPosition.X++;
                    SetTile(' ');
                }
                else if (command is CompositeCommand)
                {
                    var compositeCommand = command as CompositeCommand;
                    FollowPath(compositeCommand._container.Commands);
                }
            }
        }

        public void SetTile(char c)
        {
            SetTile(_currentPosition.X, _currentPosition.Y, c);
        }

        public void SetTile(int x, int y, char c)
        {
            Layout[x + _def.Width * y] = c;
        }

        public char GetTile(int x, int y)
        {
            return Layout[x + _def.Width * y];
        }

        private void SetBlockingTiles()
        {
            for (int x = 0; x < _def.Width; x++)
            {
                for (int y = 0; y < _def.Height; y++)
                {
                    var tile = GetTile(x, y);
                    if (IsPathTile(tile)) continue;

                    var hasLeftNeighbor = CheckLeftNeighbour(x, y);
                    var hasRightNeighbor = CheckRightNeighbour(x, y);
                    var hasTopNeighbor = CheckTopNeighbour(x, y);
                    var hasBottomNeighbor = CheckBottomNeighbour(x, y);

                    var blockingTile = GetBlockingTile(hasLeftNeighbor, hasRightNeighbor, hasTopNeighbor,
                        hasBottomNeighbor);
                    SetTile(x, y, blockingTile);
                }
            }
        }

        private bool IsPathTile(char tile)
        {
            return tile == ' ' || tile == '@' || tile == '©';
        }

        private char GetBlockingTile(
            bool hasLeftNeighbor, bool hasRightNeighbor, bool hasTopNeighbor, bool hasBottomNeighbor)
        {
            if (!hasLeftNeighbor && !hasRightNeighbor && hasTopNeighbor && hasBottomNeighbor) return '│';
            if (hasLeftNeighbor && hasRightNeighbor && !hasTopNeighbor && !hasBottomNeighbor) return '─';
            if (!hasLeftNeighbor && hasRightNeighbor && !hasTopNeighbor && hasBottomNeighbor) return '┌';
            if (hasLeftNeighbor && !hasRightNeighbor && !hasTopNeighbor && hasBottomNeighbor) return '┐';
            if (!hasLeftNeighbor && hasRightNeighbor && hasTopNeighbor && !hasBottomNeighbor) return '└';
            if (hasLeftNeighbor && !hasRightNeighbor && hasTopNeighbor && !hasBottomNeighbor) return '┘';
            if (!hasLeftNeighbor && hasRightNeighbor && hasTopNeighbor && hasBottomNeighbor) return '├';
            if (hasLeftNeighbor && !hasRightNeighbor && hasTopNeighbor && hasBottomNeighbor) return '┤';
            if (hasLeftNeighbor && hasRightNeighbor && !hasTopNeighbor && hasBottomNeighbor) return '┬';
            if (hasLeftNeighbor && hasRightNeighbor && hasTopNeighbor && !hasBottomNeighbor) return '┴';
            if (hasLeftNeighbor && hasRightNeighbor && hasTopNeighbor && hasBottomNeighbor) return '┼';
            if (hasLeftNeighbor || hasRightNeighbor) return '─';
            return '│';
        }

        private bool CheckLeftNeighbour(int x, int y)
        {
            if (x == 0) return false;
            var leftTile = GetTile(x - 1, y);
            if (IsPathTile(leftTile)) return false;
            return true;
        }

        private bool CheckRightNeighbour(int x, int y)
        {
            if (x == _def.Width - 1) return false;
            var rightTile = GetTile(x + 1, y);
            if (IsPathTile(rightTile)) return false;
            return true;
        }

        private bool CheckTopNeighbour(int x, int y)
        {
            if (y == 0) return false;
            var topTile = GetTile(x, y - 1);
            if (IsPathTile(topTile)) return false;
            return true;
        }
        private bool CheckBottomNeighbour(int x, int y)
        {
            if (y == _def.Height - 1) return false;
            var bottomTile = GetTile(x, y + 1);
            if (IsPathTile(bottomTile)) return false;
            return true;
        }
    }
}