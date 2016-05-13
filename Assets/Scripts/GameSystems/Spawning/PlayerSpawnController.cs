using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/*\
|*| This class is responsible for controlling when players respawn
|*| aswell as selecting their respawn points.
\*/
public class PlayerSpawnController : MonoBehaviour
{
    public GameObject spawnPointGroup;
    public UnityEngine.GameObject player1;
    public UnityEngine.GameObject player2;
    public UnityEngine.GameObject player3;
    public UnityEngine.GameObject player4;

    List<GameObject> activePlayers = new List<GameObject>();
    List<GameObject> deadPlayers = new List<GameObject>();
    List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    System.Random rand = new System.Random();

    void Start()
    {
        // Find all player gameobjects in the scene and add them to the spawn list.

        //var players = FindObjectsOfType(typeof (HealthController)) as HealthController[];
        //foreach (Object obj in FindObjectsOfType<HealthController>())   
        //{
        //    activePlayers.Add((obj as HealthController).gameObject);
        //}

        activePlayers.Add(player1);
        activePlayers.Add(player2);
        activePlayers.Add(player3);
        activePlayers.Add(player4);

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

        foreach (var player in deadPlayers.ToArray()) Spawn(player);
    }

    // Move player from active to inactive list.
    public void RegisterAsDead(GameObject player)
    {
        if (activePlayers.IndexOf(player) != -1)
        {
            activePlayers.Remove(player);
            deadPlayers.Add(player);
        }     
    }

    // Select a random respawn point, parse the information to the player's 
    // DeathController and move them from the inactive to the active list.
    void Spawn(GameObject player)
    {
        int idx = rand.Next(spawnPoints.Count);

        SpawnPoint spawn = spawnPoints[idx];

        player.GetComponent<DeathController>().Respawn(spawn.GetPosition(), spawn.GetRotation(), spawn.GetGravity());
 
        deadPlayers.Remove(player);
        activePlayers.Add(player);

        Debug.Log("Spawned: " + player.name);
    }

}
