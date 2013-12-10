namespace HomingRobot.Menus
{
    public abstract class MenuOption
    {
        public abstract string Text { get;}

        public abstract void Execute(params object[] arguments);

        public abstract void Left();

        public abstract void Right();
    }
}