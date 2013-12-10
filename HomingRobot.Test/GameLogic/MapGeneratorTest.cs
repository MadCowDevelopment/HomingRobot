using System.Collections.Generic;
using HomingRobot.GameLogic;
using Xunit;

namespace HomingRobot.Test.GameLogic
{

    public class MapGeneratorTest
    {
        private static readonly char[] TestLayout =
        {
            '┌', '─', '─', '┐',
            '│', ' ', '@', '│',
            '│', ' ', '─', '┤',
            '│', ' ', ' ', '│',
            '├', '─', ' ', '│',
            '│', '©', ' ', '│',
            '└', '─', '─', '┘'
        };

        [Fact]
        public void ShouldCreateCorrectLayout()
        {
            var generator = new MapGenerator();

            var commandContainer1 = new CommandContainer();
            var commandContainer2 = new CommandContainer();

            commandContainer1.Commands = new List<Command>
            {
                new RightCommand(null),
                new CompositeCommand(commandContainer2, null),
                new LeftCommand(null),
                new CompositeCommand(commandContainer2, null),
                new RightCommand(null)
            };

            commandContainer2.Commands = new List<Command>
            {
                new UpCommand(null),
                new UpCommand(null)
            };

            var result = generator.CreateMap(commandContainer1.Commands);

            Assert.Equal(TestLayout, result.Layout);
        }
    }
}
