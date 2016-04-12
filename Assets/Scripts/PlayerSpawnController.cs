using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerSpawnController : MonoBehaviour
{
    List<GameObject> activePlayers = new List<GameObject>();
    List<GameObject> deadPlayers = new List<GameObject>();
    List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    System.Random rand = new System.Random();

    void Start()
    {
        // Find all player gameobjects in the scene and add them to the spawn list
        foreach (Object obj in FindObjectsOfType<HealthController>())
        {
            //deadPlayers.Add((obj as HealthController).gameObject);
        }
        
        foreach (var spawnPoint in FindObjectsOfType<SpawnPoint>())
        {
            spawnPoints.Add(spawnPoint);
            spawnPoints.Last().gameObject.SetActive(false);
        }
        
    }
    void Update()
    {
        if (deadPlayers.Count == 0) return;

        foreach (var player in deadPlayers) Spawn(player);
    }

    public void registerAsDead(GameObject player)
    {
        if (activePlayers.IndexOf(player) != -1)
        {
            activePlayers.Remove(player);
            deadPlayers.Add(player);
        }     
    }

    void Spawn(GameObject player)
    {
        int idx = rand.Next(spawnPoints.Count);

        SpawnPoint spawn = spawnPoints[idx];

        player.transform.position = spawn.GetPosition();
        player.transform.rotation = spawn.GetRotation();
        player.GetComponent<GravityHandler>().Gravity = spawn.GetGravity();
        player.gameObject.SetActive(true);

        deadPlayers.Remove(player);
        activePlayers.Add(player);
    }

}
