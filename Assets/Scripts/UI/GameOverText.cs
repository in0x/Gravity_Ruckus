using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverText : MonoBehaviour
{
    public GameObject NameText;
    public GameObject KillText;
    public GameObject DeathText;

    public int numberOfThisPlayer = -1;

    Text[] texts;

	void Start ()
    {
        texts = new Text[3];
        texts[0] = NameText.GetComponent<Text>();
        texts[1] = KillText.GetComponent<Text>();
        texts[2] = DeathText.GetComponent<Text>();

        string[] names = GameOverInformation.names;
        for (int i = 0; i < GameOverInformation.names.Length; i++)
        {
            string name = names[i];
            if (System.Convert.ToInt32(name.Remove(0, 6)) == numberOfThisPlayer)
            {
                texts[0].text = names[i];
            }

        }
    }

    void Update ()
    {
	
	}
}
