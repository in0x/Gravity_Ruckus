using UnityEngine;

public class MeleeWeapon : MonoBehaviour, ICanShoot
{
    public float m_fRange = 0;
    public float m_fTimeToDrawRay = 0;
    public float m_fixedDamage = 0;
    public float m_VelocityMultiplier = 5;

    [SerializeField]
    float m_cooldown = 2f; 
    public float Cooldown
    {
        get { return m_cooldown; }
        set { }
    }

    bool m_available;
    public bool Available
    {
        get { return true; }
        set { m_available = value; }
    }

    float m_TimeSinceShot = 0;

    LineRenderer m_rayRenderer;
    Transform m_parentCamera;
    Rigidbody m_parentBody;

    public void Start()
    {
        m_rayRenderer = GetComponent<LineRenderer>();
        m_rayRenderer.SetWidth(0.1f, 25f);
        m_rayRenderer.enabled = false;

        // Find the transform of the parents camera component
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") m_parentCamera = child;
        }
        m_parentBody = GetComponentInParent<Rigidbody>();
    }

    public void Update()
    {
        if (m_rayRenderer.enabled)
        {
            m_TimeSinceShot += Time.deltaTime;

            if (m_TimeSinceShot > m_fTimeToDrawRay)
            {
                m_rayRenderer.enabled = false;
                m_TimeSinceShot = 0;
            }
        }
    }

    public void Shoot()
    {
        Vector3 origin = m_parentCamera.transform.position;
        Vector3 fwd = m_parentCamera.transform.forward;
        Ray ray = new Ray(origin, fwd);

        RaycastHit collisionInfo;

        m_rayRenderer.SetPosition(0, origin);
        
        if (Physics.SphereCast(m_parentCamera.transform.position, 25, fwd, out collisionInfo, m_fRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            m_rayRenderer.SetPosition(1, collisionInfo.point);
            Vector3 effectiveVelocity;

            if (Vector3.Angle(m_parentBody.velocity, fwd) < 90)
            {
                effectiveVelocity = Vector3.Project(m_parentBody.velocity, fwd);
            }
            else
            {
                effectiveVelocity = Vector3.zero;
            }
            collisionInfo.collider.gameObject.SendMessage("RecieveDamage", new DamageInfo(gameObject, m_fixedDamage + effectiveVelocity.sqrMagnitude*m_VelocityMultiplier), SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            m_rayRenderer.SetPosition(1, ray.GetPoint(m_fRange));
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void GetAmmoState(out int currentAmmo, out int maxAmmo)
    {
        var ammoComp = GetComponent<AmmoComponent>();
        maxAmmo = ammoComp.m_maxAmmo;
        currentAmmo = ammoComp.CurrentAmmo;
    }
}
