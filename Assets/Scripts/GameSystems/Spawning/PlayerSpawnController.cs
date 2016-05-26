using UnityEngine;
using UnityEngine.UI;
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
    
    List<GameObject> activePlayers = new List<GameObject>();
    List<GameObject> deadPlayers = new List<GameObject>();
    List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    Dictionary<string, RespawnTimer> respawnTimers;

    System.Random rand = new System.Random();

    void Start()
    {
        activePlayers.Add(player1);
        activePlayers.Add(player2);
        activePlayers.Add(player3);
        activePlayers.Add(player4);

        respawnTimers = new Dictionary<string, RespawnTimer>(activePlayers.Count);
        RespawnTimer[] timers = FindObjectsOfType<RespawnTimer>();

        for (int i = 0; i < timers.Length; i++)
        {
            respawnTimers.Add(activePlayers[i].name, timers[i]);
            timers[i].gameObject.SetActive(false);
        }

        foreach (SpawnPoint spawn in spawnPointGroup.GetComponentsInChildren<SpawnPoint>())
        {
            spawnPoints.Add(spawn);
            spawn.gameObject.SetActive(false);
        }
    }

    // Check if there are any inactive players, if yes, respawn them.
    void Update()
    {
        if (deadPlayers.Count == 0) return;

        foreach (var player in deadPlayers)
        {
            if (respawnTimers[player.name].ReadyToSpawn)
            {
                Spawn(player);
            }
        }
    }

    // Move player from active to inactive list.
    public void RegisterAsDead(GameObject player)
    {
        if (activePlayers.IndexOf(player) != -1)
        {
            activePlayers.Remove(player);
            deadPlayers.Add(player);
            respawnTimers[player.name].gameObject.SetActive(true);
        }
    }

    // Select a random respawn point, parse the information for the player's 
    // DeathController and move them from the inactive to the active list.
    void Spawn(GameObject player)
    {
        int idx = rand.Next(spawnPoints.Count);

        SpawnPoint spawn = spawnPoints[idx];

        player.GetComponent<DeathController>().Respawn(spawn.GetPosition(), spawn.GetRotation(), spawn.GetGravity());

        deadPlayers.Remove(player);
        activePlayers.Add(player);
    }

}