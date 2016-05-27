using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public GameObject m_nameTextGameObj;
    public GameObject m_killTextGameObj;
    public GameObject m_deathTextGameObj;

    public int m_numberOfThisPlayer = -1;

    Text[] m_texts;

	void Start ()
    {
        m_texts = new Text[3];
        m_texts[0] = m_nameTextGameObj.GetComponent<Text>();
        m_texts[1] = m_killTextGameObj.GetComponent<Text>();
        m_texts[2] = m_deathTextGameObj.GetComponent<Text>();

        string[] names = GameOverInformation.namesArr;
        int[] kills = GameOverInformation.killsArr;
        int[] deaths = GameOverInformation.deathsArr;

        for (int i = 0; i < GameOverInformation.namesArr.Length; i++)
        {
            string name = names[i];
            if (System.Convert.ToInt32(name.Remove(0, 6)) == m_numberOfThisPlayer)
            {
                m_texts[0].text = names[i];
                m_texts[1].text = kills[i].ToString();
                m_texts[2].text = deaths[i].ToString();

                foreach (Text text in m_texts) text.color = MatchProperties.colorValues[(int)MatchProperties.playerColors[3 - i]];
            }
        }
    }
}
