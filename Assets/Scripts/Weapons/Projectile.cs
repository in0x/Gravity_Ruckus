using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed;

    public float impactDmg = 35f;
    public float splashDmg = 10f;

    public float m_fExplRadius = 5.0f;
    public float m_fExplPower = 1000.0f;

    Rigidbody body;
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity *= speed;
	}
    
    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        
        // Also expensive
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplRadius);

        // Careful, this is expensive as it uses reflection
        // This, will however only trigger on colliders that also have IDamageRecievers in their hierarchy level, meaning that
        // the players main capsule collider will not be affected
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", impactDmg, SendMessageOptions.DontRequireReceiver);

        foreach (Collider hit in colliders)
        {
            // Also expensive
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.gameObject.SendMessageUpwards("RecieveDamage", splashDmg, SendMessageOptions.DontRequireReceiver);           
                rb.AddExplosionForce(m_fExplPower, explosionPos, m_fExplRadius, 3.0F);
            }
        }

        gameObject.SetActive(false);
    }
    
}
