using UnityEngine;
using System.Collections;

public class TransitionController : MonoBehaviour
{
    public IPadGUIElement m_currentGUIElement;
    MenuInputController inputController;

    void Start()
    {
        inputController = FindObjectOfType<MenuInputController>();
        m_currentGUIElement.Activate();
    }

    void Update()
    {
        if (inputController.GetDPad("DRight") || inputController.GetAxis("StickHor") > 0)
        {
            Transition transition = m_currentGUIElement.GoRight();
            m_currentGUIElement = transition();

            while (m_currentGUIElement == null) m_currentGUIElement = transition();
        }
        else if (inputController.GetDPad("DLeft") || inputController.GetAxis("StickHor") < 0)
        {
            Transition transition = m_currentGUIElement.GoLeft();
            m_currentGUIElement = transition();

            while (m_currentGUIElement == null) m_currentGUIElement = transition();
        }
        else if (inputController.GetDPad("DUp") || inputController.GetAxis("StickVert") > 0)
        {
            Transition transition = m_currentGUIElement.GoUp();
            m_currentGUIElement = transition();

            while (m_currentGUIElement == null) m_currentGUIElement = transition();
        }
        if (inputController.GetDPad("DDown") || inputController.GetAxis("StickVert") < 0)
        {
            Transition transition = m_currentGUIElement.GoDown();
            m_currentGUIElement = transition();

            while (m_currentGUIElement == null) m_currentGUIElement = transition();
        }
    }
}
