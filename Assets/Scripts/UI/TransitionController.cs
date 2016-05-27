using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public IPadGUIElement m_currentGUIElement;
    MenuInputController m_inputController;
    Transition m_lastTransition;

    void Start()
    {
        m_inputController = FindObjectOfType<MenuInputController>();
    }

    void Update()
    {
        if (m_currentGUIElement == null)
        {
            m_currentGUIElement = m_lastTransition();
            return;
        }
        else if (m_inputController.GetDPad("DRight") || m_inputController.GetAxis("StickHor") > 0)
        {
            m_lastTransition = m_currentGUIElement.GoRight();
            m_currentGUIElement = m_lastTransition();
        }
        else if (m_inputController.GetDPad("DLeft") || m_inputController.GetAxis("StickHor") < 0)
        {
            m_lastTransition = m_currentGUIElement.GoLeft();
            m_currentGUIElement = m_lastTransition();
        }
        else if (m_inputController.GetDPad("DUp") || m_inputController.GetAxis("StickVert") > 0)
        {
            m_lastTransition = m_currentGUIElement.GoUp();
            m_currentGUIElement = m_lastTransition();          
        }
        else if (m_inputController.GetDPad("DDown") || m_inputController.GetAxis("StickVert") < 0)
        {
            m_lastTransition = m_currentGUIElement.GoDown();
            m_currentGUIElement = m_lastTransition();       
        }
    }
}
