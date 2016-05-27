using UnityEngine;
using UnityEngine.UI;

public enum PlayerColor
{
    red = 0,
    green = 1, 
    blue = 2, 
    hotpink = 3
}

public class PlayerSelectionField : IPadGUIElement
{
    public int m_padNumber;
    public PlayerColor m_color;

    public Sprite m_inactiveImg;
    public Sprite m_activeImg;

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

    protected override void Update()
    {
        if (m_active)
        {
            if (m_inputController.GetButtonDown("RightBumper"))
            {
                int col = (((int)m_color) + 1);
                if (col == 5) col = 0;

                m_color = (PlayerColor)col;
                setButtonColor();
            }
            else if (m_inputController.GetButtonDown("LeftBumper"))
            {
                int col = (((int)m_color) - 1);
                if (col == -1) col = 4;

                m_color = (PlayerColor)col;
                setButtonColor();
            }

        }
    }

    void setButtonColor()
    {
        switch (m_color)
        {
            case PlayerColor.blue:
                m_button.image.color = new Color(0, 0, 1, 0.7843137254901f);
                break;

            case PlayerColor.green:
                m_button.image.color = new Color(0, 1, 0, 0.7843137254901f);
                break;

            case PlayerColor.hotpink:
                m_button.image.color = new Color(1, 0.0784f, 0.576f, 0.7843137254901f);
                break;

            case PlayerColor.red:
                m_button.image.color = new Color(1, 0, 0, 0.7843137254901f);
                break;

            default:
                break;
        }
    }

    public override void Activate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.7843137254901f);
        Debug.Log("PlayerSelectionField activated");

        m_button.image.sprite = m_activeImg;
        setButtonColor();

        m_active = true;
    }

    public override void Deactivate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
        Debug.Log("PlayerSelectionField deactivated");

        m_button.image.sprite = m_inactiveImg;
        m_button.image.color = new Color(1, 1, 1, 0.7843137254901f);

        m_active = false;
    }
}
