using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour
{
    PlayerSpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<PlayerSpawnController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("d")) Die();
    }

    public void Die()
    {
        foreach (Transform tf in gameObject.transform)
        {
            GameObject child = tf.gameObject;
            
            if (child.name == "SceneCamera")
            {
                Debug.Log("Activated: " + child.name);
                child.SetActive(true);
            }
            else
            {
                Debug.Log("Dectivated: " + child.name);
                child.SetActive(false);
            }
                     
        }

        spawnController.registerAsDead(this.gameObject);
    }
}
