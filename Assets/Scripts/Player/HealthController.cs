using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    private float m_health = 100f;

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
    }

    void Start ()
    {
        ConnectAllDamageRecievers();
	}
	void Update ()
    {
	
	}

    public void ApplyDamage(float dmg)
    {
        m_health -= dmg;
    }
    
}
