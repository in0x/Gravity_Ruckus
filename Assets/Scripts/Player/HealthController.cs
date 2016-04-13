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
        if (Input.GetKeyDown("d")) m_health -= 10; // For health pickup debugging purposes.
        if (m_health <= 0) deathController.Die();
	}

    public void ApplyDamage(float dmg)
    {
        m_health -= dmg;
    }

    // No we are not using negative damage for healing.
    public bool Heal(float hp)
    {
        if (m_health == 100) return false;

        m_health += hp;
        if (m_health > 100) m_health = 100;

        return true;
    }

    public void Refill()
    {
        m_health = 100;
    }
}
