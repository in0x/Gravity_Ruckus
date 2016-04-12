using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    private float m_health = 100f;

    DeathController deathController;

    public float Health
    {
        get
        {
            return m_health;
        }

        private set { }
    }

    // This finds all IDamageRecievers in the Players children
    // and sets this as their HealthController, thereby establishing a 
    // connection with them. This is an expensive operation and should
    // only ever be called on start-up
    private void ConnectAllDamageRecievers()
    {
        foreach (var reciever in GetComponentsInChildren<IDamageReciever>())
        {
            reciever.HealthController = this;
        }

        deathController = GetComponent<DeathController>();
    }

    void Start ()
    {
        ConnectAllDamageRecievers();
	}
	void Update ()
    {
        if (m_health <= 0) deathController.Die();
	}

    public void ApplyDamage(float dmg)
    {
        m_health -= dmg;
    }
    
}
