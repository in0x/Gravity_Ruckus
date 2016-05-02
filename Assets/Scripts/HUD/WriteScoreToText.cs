using UnityEngine;
using UnityEngine.UI;

public class WriteScoreToText : MonoBehaviour
{
    public Text text;
    public GameObject player;

    ScoreController scoreControl;

    void Start()
    {
        scoreControl = FindObjectOfType<ScoreController>();
    }
    void Update()
    {
        Stats playerStats = scoreControl.GetStats(player);

        text.text = "Frags: " + playerStats.kills;
    }
}
