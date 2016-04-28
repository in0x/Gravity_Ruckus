using UnityEngine;

public class BounceProjectile : MonoBehaviour
{
    public float m_dmg;
    public float m_speed = 1f;
    public int m_bounces;

    int m_currentBounces;
    
    Rigidbody m_body;

    void Start() {}

    void OnEnable()
    {
        if (m_body == null) m_body = GetComponent<Rigidbody>();

        /*
            Note that the speed multiplier is currently being added by the gun that fires the projectile,
            as multiplying it here would just mean it being overwritten when the gun sets the projectiles
            velocity. A solution would be to move bullet velocity and speed mult into the same object.
        */
        m_currentBounces = m_bounces;
    }

    void Update()
    {
        if (m_currentBounces <= 0) gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collision)
    {
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", m_dmg, SendMessageOptions.DontRequireReceiver);

        if (collision.collider.gameObject.tag == "Player")
        {
            m_currentBounces = 0;
        }
        else
        {
            m_currentBounces--;
            gameObject.transform.rotation = Quaternion.LookRotation(m_body.velocity) * Quaternion.Euler(90, 0, 0);
        }
    }
    
}
