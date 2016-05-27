using UnityEngine;

public class BlobProjectile : MonoBehaviour, IDamageSender {

    public float impactDmg = 35f;
    public float splashDmg = 10f;

    public int m_fFlightTimer = 20;
    public int m_fDecayTimer = 1000;

    Rigidbody m_body;
    GameObject m_sourceWeapon;
    int m_fCurrentFlightTimer;
    int m_fCurrentDecayTimer;   
    public GameObject SourceWeapon
    {
        get { return m_sourceWeapon; }
        set { m_sourceWeapon = value; }
    }
    
    void OnEnable()
    {
        if (m_body == null) m_body = GetComponent<Rigidbody>();
        m_fCurrentFlightTimer = m_fFlightTimer;
        m_fCurrentDecayTimer = m_fDecayTimer;
        m_body.angularVelocity = Random.insideUnitSphere * 5;
    }

    void FixedUpdate()
    {
        if (m_fCurrentFlightTimer > 0)
        {
            m_fCurrentFlightTimer--;
        }
        else
        {
            m_body.velocity = Vector3.zero;
            if (m_fCurrentDecayTimer > 0)
            {
                m_fCurrentDecayTimer--;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {       
        DamageInfo impactInfo = new DamageInfo(SourceWeapon, impactDmg);
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", impactInfo, SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(false);
    }
}
