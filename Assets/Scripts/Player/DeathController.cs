using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour
{
    PlayerSpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<PlayerSpawnController>();
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        spawnController.registerAsDead(this.gameObject);
    }
}
