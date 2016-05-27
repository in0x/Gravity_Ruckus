using UnityEngine;
using System.Collections.Generic;

public class HomingComponent : MonoBehaviour
{
    public float m_strength = 0.5f;
    public float m_sphereSize = 100;
    public float m_fTimer = 5;
    public GameObject m_shooter;

    float m_currentTimer;
    List<GameObject> chestColliders; 
    Rigidbody rb;
    
	void Start ()
	{
        chestColliders = new List<GameObject>(GameObject.FindGameObjectsWithTag("ChestCollider"));
	    rb = GetComponent<Rigidbody>();
	}
	
    void Awake()
    {
        m_currentTimer = m_fTimer;
    }

    void FixedUpdate()
    {
        if (m_shooter == null) return;

        if (m_currentTimer > 0)
        {
            m_currentTimer--;
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
            if (player.transform.root == m_shooter.transform)
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

        if (closestDistance < m_sphereSize)
        {
            float speed = rb.velocity.magnitude;
            rb.velocity = Vector3.Lerp(rb.velocity.normalized, (closestPlayer.transform.position - position).normalized, m_strength).normalized;
            rb.velocity *= speed;
        }
    }
}
