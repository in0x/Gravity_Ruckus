using UnityEngine;

public class EmptyTransition : MonoBehaviour, ITransitioner
{
    IPadGUIElement m_self;

    void Start()
    {
        m_self = GetComponent<IPadGUIElement>();
    }

    public Transition Execute()
    {
        return delegate
        {
            return m_self;
        };
    }
}
