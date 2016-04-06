using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Vector3 m_Velocity;
    public Vector3 m_Target;
    public float speed = 1000;

    public float m_fExplRadius = 5.0f;
    public float m_fExplPower = 1000.0f;

    Rigidbody body;
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
	}
    
    void OnCollisionEnter(Collision collision)
    {
        // Disable this for pogo fun
        Destroy(gameObject);

        Vector3 explosionPos = transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplRadius);

        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.GetType());
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(m_fExplPower, explosionPos, m_fExplRadius, 3.0F);
            }
        }
    }
    
}
