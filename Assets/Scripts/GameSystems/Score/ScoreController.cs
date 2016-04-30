using UnityEngine;
using System.Collections.Generic;

/*\ 
|*| Holds all data related to a players score.
|*| The base version holds kills and deaths.
|*| Possible extensions include for example some 
|*| sort of overall score (think TF2 or CS:GO).
\*/
class Score
{
    int m_kills = 0;
    int m_deaths = 0;

    public void AddKill()
    {
        m_kills++;
    }

    public void LoseKill()
    {
        m_kills--;
    }

    public void AddDeath()
    {
        m_deaths++;
    }
}

public class ScoreController : MonoBehaviour
{
    // Key: Player, Value: Their Score
    Dictionary<GameObject, Score> m_scoreTable;
    
    // Holds all player's DeathControllers.
    List<DeathController> m_deathControllers;

	void Start ()
    {
        m_scoreTable = new Dictionary<GameObject, Score>();

        foreach (Object obj in FindObjectsOfType<DeathController>())
        {
            var controller = obj as DeathController;
            m_deathControllers.Add(controller);

            m_scoreTable.Add(controller.gameObject, new Score());
        }
    }
	
	void Update ()
    {
	
	}

    public void ProcessKill(GameObject sender, DamageInfo killInfo)
    {
        var bla = m_scoreTable[sender];
    }
}
