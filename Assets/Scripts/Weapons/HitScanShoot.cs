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

    LineRenderer rayRenderer;
    Transform parentCamera;

    // Used to track ammo of weapon
    AmmoComponent ammoComp;

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
        rayRenderer = GetComponent<LineRenderer>();
        rayRenderer.SetWidth(0.1f, 0.1f);
        rayRenderer.enabled = false;

        // Find the transform of the parents camera component
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") parentCamera = child;
        }

        ammoComp = GetComponent<AmmoComponent>();

        m_sourceWeapon = gameObject;
    }

    public void Update()
    {
        // Check if ray has exceeded time to live
        if (rayRenderer.enabled)
        {
            m_TimeSinceShot += Time.deltaTime;

            if (m_TimeSinceShot > m_fTimeToDrawRay)
            {
                rayRenderer.enabled = false;
                m_TimeSinceShot = 0;
            }
        }
    }

    public void Shoot()
    {
        if (ammoComp.UseAmmo(m_ammoUsedOnShot) == 0)
        {
            Debug.Log("Out of ammo");
            return;
        };

        // Get origin of shooter and look direction via camera transform
        Vector3 origin = parentCamera.transform.position;
        Vector3 fwd = parentCamera.transform.forward;
        Ray ray = new Ray(origin, fwd);

        RaycastHit collisionInfo;

        rayRenderer.SetPosition(0, origin);

        // Raycast into the scene
        if (Physics.Raycast(parentCamera.transform.position, fwd, out collisionInfo, m_fRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            // Draw the ray to the point of collision
            rayRenderer.SetPosition(1, collisionInfo.point);

            DamageInfo dmgInfo = new DamageInfo(m_sourceWeapon, 50f);

            // Careful, this is expensive as it uses reflection
            collisionInfo.collider.gameObject.SendMessage("RecieveDamage", dmgInfo, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // If no hit, draw the point until max range
            rayRenderer.SetPosition(1, ray.GetPoint(m_fRange));
        }

        rayRenderer.enabled = true;
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
