using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class HomingComponent : MonoBehaviour
{

    public float Strength = 0.5f;
    public float SphereSize = 100;
    public float Timer = 5;

    public GameObject shooter;

    private float currentTimer;

    //private List<GameObject> players;
    private List<GameObject> chestColliders; 
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
        chestColliders = new List<GameObject>(GameObject.FindGameObjectsWithTag("ChestCollider"));
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        currentTimer = Timer;
    }

    void FixedUpdate()
    {
        if (currentTimer > 0)
        {
            currentTimer--;
            return;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        Vector3 position = transform.position;
        GameObject closestPlayer = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject player in chestColliders)
        {
            if (player.transform.root == shooter.transform)
            {
                continue;
            }
            float currentDistance = (position - player.transform.position).sqrMagnitude;
            if (currentDistance < closestDistance)
            {
                closestPlayer = player;
                closestDistance = currentDistance;
            }
        }
        if (closestDistance < SphereSize)
        {
            float speed = rb.velocity.magnitude;
            rb.velocity = Vector3.Lerp(rb.velocity.normalized, (closestPlayer.transform.position - position).normalized, Strength).normalized;
            rb.velocity *= speed;
        }
    }
}
