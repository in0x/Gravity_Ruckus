using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum PlayerColor
{
    red,
    green, 
    blue, 
    hotpink
}

public class PlayerSelectionField : IPadGUIElement
{
    public int m_padNumber;
    public PlayerColor m_color;

    MenuInputController m_inputController;
    Button m_button;
    
    protected override void Start()
    {
        base.Start();
        m_inputController = gameObject.AddComponent<MenuInputController>();
        m_button = GetComponent<Button>();
    }

    public void SetControllerNumber(int controllerID)
    {
        m_inputController.SetPlayerNumber(controllerID);
    }

    public int GetControllerNumber()
    {
        return m_inputController.GetPlayerNumber();
    }

    public void ready()
    {
        Debug.Log("PlayerSelectionField ready");
    }

    protected override void Update()
    {
        if (m_active)
        {
            if (m_inputController.GetButtonDown("ButtonClick")) ready();

            else if (m_inputController.GetDPad("DRight") || m_inputController.GetAxis("StickHor") > 0)
            {

            }
            else if (m_inputController.GetDPad("DLeft") || m_inputController.GetAxis("StickHor") < 0)
            {

            }
        }
    }
    

    public override void Activate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.7843137254901f);
        Debug.Log("PlayerSelectionField activated");
    }

    public override void Deactivate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
        Debug.Log("PlayerSelectionField deactivated");
    }
}
