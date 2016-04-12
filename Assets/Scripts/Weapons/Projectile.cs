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
        // Also expensive
        Destroy(gameObject);

        Vector3 explosionPos = transform.position;
        
        // Also expensive
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplRadius);

        foreach (Collider hit in colliders)
        {
            // Also expensive
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            float distance = 1f / Vector3.Distance(transform.position, rb.transform.position);
            Debug.Log(distance);

            // Careful, this is expensive as it uses reflection
            // This, however should only trigger on colliders that also have IDamageRecievers in their hierarchy level, meaning that
            // the players main capsule collider will not be affected
            hit.gameObject.SendMessage("RecieveDamage", 1f, SendMessageOptions.DontRequireReceiver);

            if (rb != null)
            {
                //rb.AddExplosionForce(m_fExplPower, explosionPos, m_fExplRadius, 3.0F);
                rb.AddForce(new Vector3(0f, 100f, 0f), ForceMode.Impulse);
            }

        }
    }
    
}
