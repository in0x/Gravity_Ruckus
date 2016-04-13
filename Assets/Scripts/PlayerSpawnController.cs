using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerSpawnController : MonoBehaviour
{
    public GameObject spawnPointGroup;

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
            activePlayers.Add((obj as HealthController).gameObject);
        }

        foreach (SpawnPoint spawn in spawnPointGroup.GetComponentsInChildren<SpawnPoint>())
        {
            spawnPoints.Add(spawn);
            spawn.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (deadPlayers.Count == 0) return;

        foreach (var player in deadPlayers.ToArray()) Spawn(player);
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
        
        foreach (Transform tf in player.gameObject.transform)
        {
            GameObject child = tf.gameObject;

            if (child.name == "SceneCamera")
            {
                Debug.Log("Dectivated: " + child.name);
                child.SetActive(false);
            }
            else
            {
                Debug.Log("Activated: " + child.name);
                child.SetActive(true);
            }
        }

        player.GetComponent<HealthController>().Refill();
        deadPlayers.Remove(player);
        activePlayers.Add(player);

        Debug.Log("Spawned: " + player.name);
    }

}
