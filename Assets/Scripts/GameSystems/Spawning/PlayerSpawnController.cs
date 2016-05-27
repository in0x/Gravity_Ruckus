using UnityEngine;
using System.Collections.Generic;

/*\
|*| This class is responsible for controlling when players respawn
|*| aswell as selecting their respawn points.
\*/
public class PlayerSpawnController : MonoBehaviour
{
    public GameObject spawnPointGroup;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    
    List<GameObject> m_activePlayers = new List<GameObject>();
    List<GameObject> m_deadPlayers = new List<GameObject>();
    List<SpawnPoint> m_spawnPoints = new List<SpawnPoint>();
    Dictionary<string, RespawnTimer> m_respawnTimers;

    System.Random rand = new System.Random();

    void Start()
    {
        m_activePlayers.Add(player1);
        m_activePlayers.Add(player2);
        m_activePlayers.Add(player3);
        m_activePlayers.Add(player4);

        m_respawnTimers = new Dictionary<string, RespawnTimer>(m_activePlayers.Count);
        RespawnTimer[] timers = FindObjectsOfType<RespawnTimer>();

        for (int i = 0; i < timers.Length; i++)
        {
            m_respawnTimers.Add(m_activePlayers[i].name, timers[i]);
            timers[i].gameObject.SetActive(false);
        }

        foreach (SpawnPoint spawn in spawnPointGroup.GetComponentsInChildren<SpawnPoint>())
        {
            m_spawnPoints.Add(spawn);
            spawn.gameObject.SetActive(false);
        }
    }

    // Check if there are any inactive players, if yes, respawn them.
    void Update()
    {
        if (m_deadPlayers.Count == 0) return;

        for (int i = 0; i < m_deadPlayers.Count; i++)
        {
            if (m_respawnTimers[m_deadPlayers[i].name].ReadyToSpawn)
            {
                Spawn(m_deadPlayers[i]);
            }
        }
    }

    // Move player from active to inactive list.
    public void RegisterAsDead(GameObject player)
    {
        if (m_activePlayers.IndexOf(player) != -1)
        {
            m_activePlayers.Remove(player);
            m_deadPlayers.Add(player);
            m_respawnTimers[player.name].gameObject.SetActive(true);
        }
    }

    // Select a random respawn point, parse the information for the player's 
    // DeathController and move them from the inactive to the active list.
    void Spawn(GameObject player)
    {
        int idx = rand.Next(m_spawnPoints.Count);

        SpawnPoint spawn = m_spawnPoints[idx];

        player.GetComponent<DeathController>().Respawn(spawn.GetPosition(), spawn.GetRotation(), spawn.GetGravity());

        m_deadPlayers.Remove(player);
        m_activePlayers.Add(player);
    }

}