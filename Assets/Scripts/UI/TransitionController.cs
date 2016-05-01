using UnityEngine;
using System.Collections;

public class TransitionController : MonoBehaviour
{
    public IPadGUIElement m_currentGUIElement;
    MenuInputController inputController;
    Transition lastTransition;

    void Start()
    {
        inputController = FindObjectOfType<MenuInputController>();
        //m_currentGUIElement.Activate();
    }

    void Update()
    {
        if (m_currentGUIElement == null)
        {
            m_currentGUIElement = lastTransition();
            return;
        }
        else if (inputController.GetDPad("DRight") || inputController.GetAxis("StickHor") > 0)
        {
            lastTransition = m_currentGUIElement.GoRight();
            m_currentGUIElement = lastTransition();
        }
        else if (inputController.GetDPad("DLeft") || inputController.GetAxis("StickHor") < 0)
        {
            lastTransition = m_currentGUIElement.GoLeft();
            m_currentGUIElement = lastTransition();
            
        }
        else if (inputController.GetDPad("DUp") || inputController.GetAxis("StickVert") > 0)
        {
            lastTransition = m_currentGUIElement.GoUp();
            m_currentGUIElement = lastTransition();
            
        }
        else if (inputController.GetDPad("DDown") || inputController.GetAxis("StickVert") < 0)
        {
            lastTransition = m_currentGUIElement.GoDown();
            m_currentGUIElement = lastTransition();       
        }
    }
}
