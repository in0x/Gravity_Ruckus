using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    private float m_health = 100f;
    private float m_maxHealth = 100f;

    DeathController deathController;
    DamageInfo lastDamageTaken;
    ScreenDamageEffect screenEffect;

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
        screenEffect = GetComponentInChildren<ScreenDamageEffect>();
    }

    void Start ()
    {
        ConnectAllDamageRecievers();
	}

	void Update ()
    {
        if (m_health <= 0)
        {
            Refill();
            deathController.Die(lastDamageTaken);
        }
	}
    
    public void ApplyDamage(DamageInfo damageInfo)
    {
        Debug.Log("Damage from: " + damageInfo.senderName + " with: " + damageInfo.sourceName);
        m_health -= damageInfo.fDamage;
        lastDamageTaken = damageInfo;

        screenEffect.Activate((float)m_health / (float)m_maxHealth);
    }

    // No we are not using negative damage for healing.
    public bool Heal(float hp)
    {
        if (m_health == m_maxHealth) return false;

        m_health += hp;
        if (m_health > m_maxHealth) m_health = m_maxHealth;

        return true;
    }

    public void Refill()
    {
        m_health = m_maxHealth;
    }
}
