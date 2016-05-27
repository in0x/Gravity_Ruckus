using UnityEngine;

public class BounceProjectile : MonoBehaviour, IDamageSender
{
    public float m_dmg;
    public float m_speed = 1f;
    public int m_bounces;

    int m_currentBounces;
    
    Rigidbody m_body;

    // Used for information that is send to damage reciever 
    // about the sender and the weapon used.
    GameObject m_sourceWeapon;
    DamageInfo m_damageInfo;

    public GameObject SourceWeapon
    {
        get { return m_sourceWeapon; }
        set
        {
            m_sourceWeapon = value;
        }
    }
    
    void Start() {}

    void OnEnable()
    {
        if (m_body == null) m_body = GetComponent<Rigidbody>();
        m_currentBounces = m_bounces;
    }

    void Update()
    {
        if (m_currentBounces <= 0) gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collision)
    {
        DamageInfo dmgInfo = new DamageInfo(SourceWeapon, m_dmg);
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", dmgInfo, SendMessageOptions.DontRequireReceiver);

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
