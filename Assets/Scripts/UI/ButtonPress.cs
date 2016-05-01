using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public MenuInputController inputController;
    public string TriggerAction;
    public Button button;

    void Start()
    {
        inputController = FindObjectOfType<MenuInputController>();
    }

	void Update ()
    {
        if (inputController.GetButtonDown(TriggerAction)) button.onClick.Invoke();
	}
}
