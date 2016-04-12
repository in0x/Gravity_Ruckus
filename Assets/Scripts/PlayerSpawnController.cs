using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawnController : MonoBehaviour
{
    List<GameObject> activePlayers = new List<GameObject>();
    List<GameObject> deadPlayers = new List<GameObject>();
    List<SpawnPoint> spawns = new List<SpawnPoint>();

    System.Random rand = new System.Random();

    void Start()
    {
        // Find all player gameobjects in the scene and add them to the spawn list
        foreach (Object obj in FindObjectsOfType<HealthController>())
        {
            deadPlayers.Add((obj as HealthController).gameObject);
        }        
    }
    void Update()
    {
        if (deadPlayers.Count == 0) return;

        foreach (var player in deadPlayers) spawn(player);
    }

    public void registerAsDead(GameObject player)
    {
        if (activePlayers.IndexOf(player) != -1)
        {
            activePlayers.Remove(player);
            deadPlayers.Add(player);
        }     
    }

    void spawn(GameObject player)
    {
        rand.Next(0, spawns.Count);
    }

}
