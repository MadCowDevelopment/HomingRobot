using System.Collections.Generic;

namespace HomingRobot.GameLogic
{
    internal class MapGenerator
    {
        public MapLayout CreateMap(IEnumerable<Command> commands)
        {
            var def = CreateMapLayoutDefinition(commands);
            var mapLayout = new MapLayout(def, commands);
            return mapLayout;
        }

        private MapLayoutDefinition CreateMapLayoutDefinition(IEnumerable<Command> commands)
        {
            var definition = new MapLayoutDefinition();
            CreateMapLayoutDefinitionRec(commands, definition);
            return definition;
        }

        private void CreateMapLayoutDefinitionRec(IEnumerable<Command> commands, MapLayoutDefinition def)
        {
            foreach (var command in commands)
            {
                if (command is UpCommand)
                {
                    def.CurrentY--;
                    if (def.CurrentY < def.MinY) def.MinY = def.CurrentY;
                }
                else if (command is DownCommand)
                {
                    def.CurrentY++;
                    if (def.CurrentY > def.MaxY) def.MaxY = def.CurrentY;
                }
                else if (command is LeftCommand)
                {
                    def.CurrentX--;
                    if (def.CurrentX < def.MinX) def.MinX = def.CurrentX;
                }
                else if (command is RightCommand)
                {
                    def.CurrentX++;
                    if (def.CurrentX > def.MaxX) def.MaxX = def.CurrentX;
                }
                else if (command is CompositeCommand)
                {
                    var compositeCommand = command as CompositeCommand;
                    CreateMapLayoutDefinitionRec(compositeCommand._container.Commands, def);
                }
            }
        }
    }
}
