using UnityEngine;
using UnityEngine.UI;

public class ButtonPressGUIElement : IPadGUIElement
{
    public string m_triggerAction;

    MenuInputController m_inputController;
    Button m_button;

    protected override void Start()
    {
        base.Start();
        m_inputController = FindObjectOfType<MenuInputController>();
        m_button = GetComponent<Button>();
    }

	protected override void Update ()
    {
        if (m_active)
        {
            if (m_inputController.GetButtonDown(m_triggerAction)) m_button.onClick.Invoke();          
        } 
	}

    public override void Activate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.7843137254901f);
        m_active = true;
    }

    public override void Deactivate()
    {
        Color curColor = m_button.image.color;
        m_button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
        m_active = false;
    }
}

