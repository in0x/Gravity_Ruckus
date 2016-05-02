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

    public int Kills
    {
        get { return m_kills; }
        private set { }
    }
    public int Deaths
    {
        get { return m_deaths; }
        private set { }
    }

    public void AddKill()
    {
        m_kills++;
    }

    public void DetractKill()
    {
        m_kills--;
    }

    public void AddDeath()
    {
        m_deaths++;
    }

    public override string ToString()
    {
        return "Kills: " + m_kills + " Deaths: " + m_deaths;
    }
}

/*\
|*| Used to return a players stats without
|*| exposing the actual Score Objects 
\*/
public struct Stats
{
    public Stats(int _kills, int _deaths)
    {
        kills = _kills;
        deaths = _deaths;
    }

    public int kills;
    public int deaths;
}

public class ScoreController : MonoBehaviour
{
    // Key: Player, Value: Their Score
    Dictionary<GameObject, Score> m_scoreTable;

    // Holds all player's DeathControllers.
    List<DeathController> m_deathControllers;

    void Start()
    {
        m_scoreTable = new Dictionary<GameObject, Score>();
        m_deathControllers = new List<DeathController>();

        foreach (Object obj in FindObjectsOfType<DeathController>())
        {
            var controller = obj as DeathController;
            m_deathControllers.Add(controller);
            controller.m_scoreController = this;

            m_scoreTable.Add(controller.gameObject, new Score());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (var kvp in m_scoreTable)
            {
                Debug.Log(kvp.Key.name + " " + kvp.Value);
            }
        }
    }

    // Returns a struct containing copies of the values
    // stored by the according score object.
    public Stats GetStats(GameObject player)
    {
        if (m_scoreTable.ContainsKey(player))
        {
            var curScore = m_scoreTable[player];
            return new Stats(curScore.Kills, curScore.Deaths);

        }
        return new Stats();
    }

    public void ProcessKill(GameObject sender, DamageInfo killInfo)
    {
        // Player who died.
        m_scoreTable[sender].AddDeath();

        // If killed by other player, add one kill for 
        // them, otherwise detract one from player who died.
        if (m_scoreTable.ContainsKey(killInfo.sender))
        {
            // Killed by other player.
            m_scoreTable[killInfo.sender].AddKill();
        }
        else
        {
            // Killed by environment.
            m_scoreTable[sender].DetractKill();
        }
        
    }
}
