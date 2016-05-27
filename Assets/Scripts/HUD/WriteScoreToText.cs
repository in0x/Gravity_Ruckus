using UnityEngine;
using UnityEngine.UI;

public class WriteScoreToText : MonoBehaviour
{
    public Text m_text;
    public GameObject m_player;

    ScoreController m_scoreController;

    void Start()
    {
        m_scoreController = FindObjectOfType<ScoreController>();
    }
    void Update()
    {
        Stats playerStats = m_scoreController.GetStats(m_player);
        m_text.text = "Frags: " + playerStats.kills;
    }
}
