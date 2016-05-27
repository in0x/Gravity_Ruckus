public interface ITransitioner
{
    Transition Execute();
}

public delegate IPadGUIElement Transition();