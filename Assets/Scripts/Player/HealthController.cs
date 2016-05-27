using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float m_health = 100f;
    private float m_maxHealth = 100f;

    DeathController m_deathController;
    DamageInfo m_lastDamageTaken;
    ScreenDamageEffect m_screenEffect;

    public float Health
    {
        get
        {
            return m_health;
        }

        private set { }
    }
    
    void ConnectAllDamageRecievers()
    {
        foreach (var reciever in GetComponentsInChildren<IDamageReciever>())
        {
            reciever.HealthController = this;
        }

        m_deathController = GetComponent<DeathController>();
        m_screenEffect = GetComponentInChildren<ScreenDamageEffect>();
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
            m_deathController.Die(m_lastDamageTaken);
        }
	}
    
    public void ApplyDamage(DamageInfo damageInfo)
    {
        m_health -= damageInfo.fDamage;
        m_lastDamageTaken = damageInfo;

        m_screenEffect.Activate((float)m_health / (float)m_maxHealth);
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
