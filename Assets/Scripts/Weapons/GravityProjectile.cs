using UnityEngine;

public class GravityProjectile : MonoBehaviour, IDamageSender
{
    public float m_speed;

    public float m_impactDmg = 35f;
    public float m_splashDmg = 10f;

    public float m_fExplRadius = 5.0f;
    public float m_fExplPower = 1000.0f;

    public float m_fSwitchingSpeed = 400.0f;

    GameObject m_sourceWeapon;
    public GameObject SourceWeapon
    {
        get { return m_sourceWeapon; }
        set
        {
            m_sourceWeapon = value;
        }
    }

    Rigidbody m_rigidBody;
    HomingComponent m_homingComponent;

    public void GravitySwitch(Vector3 gravity)
    {
        m_rigidBody.velocity = gravity*m_fSwitchingSpeed;
        m_homingComponent.m_sphereSize = 1000000;
        m_homingComponent.m_strength = 0.3f;
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplRadius);

        DamageInfo impactInfo = new DamageInfo(SourceWeapon, m_impactDmg);
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", impactInfo, SendMessageOptions.DontRequireReceiver);

        DamageInfo splashInfo = new DamageInfo(SourceWeapon, m_splashDmg);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Debug.Log("Collider name " + hit.gameObject.name + m_rigidBody.velocity);

            IDamageReciever dmgReciever = hit.gameObject.GetComponent<IDamageReciever>();
            
            if (dmgReciever != null) dmgReciever.RecieveDamage(splashInfo);
        }
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(m_rigidBody == null) m_rigidBody = GetComponent<Rigidbody>();
        if (m_homingComponent == null) m_homingComponent = GetComponent<HomingComponent>();
        m_homingComponent.m_strength = 0.3f;
        m_homingComponent.m_sphereSize = 10000;
        m_rigidBody.velocity *= m_speed;
    }
}
