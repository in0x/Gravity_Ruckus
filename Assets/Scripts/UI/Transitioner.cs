using UnityEngine;
using UnityEngine.UI;

public interface ITransitioner
{
    Transition Execute();
}

public delegate IPadGUIElement Transition();