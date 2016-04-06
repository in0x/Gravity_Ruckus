using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteHealthToText : MonoBehaviour
{
    public GameObject player;

    HealthController health;
    Text text;
    string playerName;

	void Start ()
    {
        health = player.GetComponent<HealthController>();
        playerName = player.name;

        text = GetComponent<Text>();
	}
	void Update ()
    {
        text.text = playerName + " Health: " + health.Health.ToString();
	}
}
