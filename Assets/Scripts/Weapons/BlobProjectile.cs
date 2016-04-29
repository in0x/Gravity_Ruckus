using UnityEngine;
using System.Collections;

public class BlobProjectile : MonoBehaviour {

    public float impactDmg = 35f;
    public float splashDmg = 10f;

    public float m_fExplRadius = 5.0f;
    public float m_fExplPower = 1000.0f;

    public int m_fFlightTimer = 20;
    public int m_fDecayTimer = 1000;

    Rigidbody m_body;
    int m_fCurrentFlightTimer;
    int m_fCurrentDecayTimer;

    void OnEnable()
    {
        if (m_body == null) m_body = GetComponent<Rigidbody>();

        /*
            Note that the speed multiplier is currently being added by the gun that fires the projectile,
            as multiplying it here would just mean it being overwritten when the gun sets the projectiles
            velocity. A solution would be to move bullet velocity and speed mult into the same object.
        */
        m_fCurrentFlightTimer = m_fFlightTimer;
        m_fCurrentDecayTimer = m_fDecayTimer;
        m_body.angularVelocity = new Vector3(Random.value,Random.value,Random.value)*5;
    }

    void FixedUpdate()
    {
        if (m_fCurrentFlightTimer > 0)
            m_fCurrentFlightTimer--;
        else
        {
            m_body.velocity = Vector3.zero;
            if (m_fCurrentDecayTimer > 0)
                m_fCurrentDecayTimer--;
            else
                gameObject.SetActive(false);
        }
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
