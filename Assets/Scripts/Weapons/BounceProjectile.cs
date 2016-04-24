using UnityEngine;

public class BounceProjectile : MonoBehaviour
{
    public float m_dmg;
    public float m_speed = 1f;
    public int m_bounces;

    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity *= m_speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (m_bounces > 0)
        {
            body.velocity = Vector3.Reflect(body.velocity, collision.other.transform.up);
        }
    }
}
