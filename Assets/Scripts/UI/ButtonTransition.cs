using UnityEngine;

public class ButtonTransition : MonoBehaviour, ITransitioner
{
    public GameObject m_nextElement;
  
    IPadGUIElement m_next;
    IPadGUIElement m_self;

    void Start()
    {
        m_self = GetComponent<IPadGUIElement>();
        m_next = m_nextElement.GetComponent<IPadGUIElement>();
    }

    public Transition Execute()
    {
        return delegate
        {
            m_self.Deactivate();
            m_next.Activate();
            return m_next;
        };
    }
}