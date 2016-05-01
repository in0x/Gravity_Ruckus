using UnityEngine;
using UnityEngine.UI;

public class ButtonPressGUIElement : IPadGUIElement
{
    MenuInputController inputController;

    public string TriggerAction;
    Button button;

    protected override void Start()
    {
        base.Start();
        inputController = FindObjectOfType<MenuInputController>();
        button = GetComponent<Button>();
    }

	protected override void Update ()
    {
        if (m_active)
        {
            if (inputController.GetButtonDown(TriggerAction))
            {
                Debug.Log("Clicked");
                button.onClick.Invoke();
            }
        } 
	}

    public override void Activate()
    {
        Color curColor = button.image.color;
        button.image.color = new Color(curColor.r, curColor.g, curColor.b, 1f);
        m_active = true;
    }

    public override void Deactive()
    {
        Color curColor = button.image.color;
        button.image.color = new Color(curColor.r, curColor.g, curColor.b, 0.7843137254901f);
        m_active = false;
    }
}

