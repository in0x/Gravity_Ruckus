using UnityEngine;
using UnityEngine.UI;

public class IPadGUIElement : MonoBehaviour
{
    public bool m_active { get; protected set; }

    public MonoBehaviour upTransition;
    public MonoBehaviour downTransition;
    public MonoBehaviour leftTransition;
    public MonoBehaviour rightTransition;

    ITransitioner m_upTrans;
    ITransitioner m_downTrans;
    ITransitioner m_leftTrans;
    ITransitioner m_rightTrans;

    protected virtual void Start()
    {
        m_upTrans = (upTransition as ITransitioner);
        m_downTrans = (downTransition as ITransitioner);
        m_leftTrans = (leftTransition as ITransitioner);
        m_rightTrans = (rightTransition as ITransitioner);
    }

    protected virtual void Update(){}

    public virtual void Activate(){}

    public virtual void Deactivate() {}

    public Transition GoLeft()
    { 
        return m_leftTrans.Execute();
    }

    public Transition GoRight()
    {
        return m_rightTrans.Execute();
    }

    public Transition GoUp()
    {
        return m_upTrans.Execute();
    }

    public Transition GoDown()
    {
        return m_downTrans.Execute();
    }
}
