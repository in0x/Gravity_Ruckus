using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverText : MonoBehaviour
{
    //MatchProperties
    public GameObject NameText;
    public GameObject KillText;
    public GameObject DeathText;

    Text[] texts;

	void Start ()
    {
        texts = new Text[3];
        texts[0] = NameText.GetComponent<Text>();
        texts[1] = KillText.GetComponent<Text>();
        texts[2] = DeathText.GetComponent<Text>();

        foreach (string name in GameOverInformation.names)
        {
            string idx = name.Remove(0, 6);
            Debug.Log(idx);
        }
    }

    void Update ()
    {
	
	}
}
