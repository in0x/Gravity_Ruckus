using UnityEngine;
using System.Collections;
        
public class HitScanShoot : MonoBehaviour, ICanShoot, IDamageSender
{
    public float m_fRange = 0;
    public float m_fTimeToDrawRay = 0;
    public int m_ammoUsedOnShot = 1;

    [SerializeField]
    float m_cooldown = 2f; 
    public float Cooldown
    {
        get { return m_cooldown; }
        set { }
    }

    float m_TimeSinceShot = 0;

    LineRenderer m_rayRenderer;
    Transform m_parentCamera;
    AmmoComponent m_ammoComp;

    // Used for information that is send to damage reciever 
    // about the sender and the weapon used.
    GameObject m_sourceWeapon;
    public GameObject SourceWeapon
    {
        get { return m_sourceWeapon; }
        set
        {
            m_sourceWeapon = value;
        }
    }

    bool m_available;
    public bool Available
    {
        get { return m_available; }
        set { m_available = value; }
    }

    public void Start()
    {
        m_rayRenderer = GetComponent<LineRenderer>();
        m_rayRenderer.SetWidth(0.1f, 0.1f);
        m_rayRenderer.enabled = false;
        
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") m_parentCamera = child;
        }

        m_ammoComp = GetComponent<AmmoComponent>();
        m_sourceWeapon = gameObject;
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
        if (m_ammoComp.UseAmmo(m_ammoUsedOnShot) == 0) return;
        
        // Get origin of shooter and look direction via camera transform
        Vector3 origin = m_parentCamera.transform.position;
        Vector3 fwd = m_parentCamera.transform.forward;
        Ray ray = new Ray(origin, fwd);

        RaycastHit collisionInfo;
        m_rayRenderer.SetPosition(0, origin);
        
        if (Physics.Raycast(m_parentCamera.transform.position, fwd, out collisionInfo, m_fRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            m_rayRenderer.SetPosition(1, collisionInfo.point);
            DamageInfo dmgInfo = new DamageInfo(m_sourceWeapon, 50f);
            collisionInfo.collider.gameObject.SendMessage("RecieveDamage", dmgInfo, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            m_rayRenderer.SetPosition(1, ray.GetPoint(m_fRange));
        }
        m_rayRenderer.enabled = true;
    }

    public void GetAmmoState(out int currentAmmo, out int maxAmmo)
    {
        var ammoComp = GetComponent<AmmoComponent>();
        maxAmmo = ammoComp.m_maxAmmo;
        currentAmmo = ammoComp.CurrentAmmo;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
