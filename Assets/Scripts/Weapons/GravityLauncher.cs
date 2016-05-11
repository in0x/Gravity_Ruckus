using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GravityLauncher : MonoBehaviour, ICanShoot, IGravityObserver
{
    // Prefab of the projectile to be launched.
    public GameObject m_projectilePrefab;
    float m_fInherentProjectileVel = 500;
    public int m_ammoUsedOnShot = 1;

    [SerializeField]
    float m_cooldown = 0.5f;

    Transform m_parentCamera;
    CapsuleCollider m_parentCollider;
    SphereCollider m_projectileCollider;

    // List to keep track of owned projectiles.
    List<PooledGameObject> m_shotProjectiles = new List<PooledGameObject>();
    ObjectPoolManager m_poolManager;

    public float Cooldown
    {
        get { return m_cooldown; }
        set { }
    }

    bool m_available;
    public bool Available
    {
        get { return m_available; }
        set { m_available = value; }
    }

    AmmoComponent ammoComp;

    void Start()
    {
        // Find the transform of the parents camera component.
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") m_parentCamera = child;
        }

        m_parentCollider = transform.parent.GetComponent<CapsuleCollider>();
        m_projectileCollider = m_projectilePrefab.GetComponent<SphereCollider>();
        m_poolManager = ObjectPoolManager.Get();
        m_shotProjectiles = new List<PooledGameObject>();
        ammoComp = GetComponent<AmmoComponent>();
    }

    void Update()
    {
        // Projectiles deactivate themselves when they are ready to be released
        // -> Release all projectiles that are inactive and remove them from
        // the list of active projectiles.
        m_shotProjectiles.RemoveAll((pooledObject =>
        {
            if (pooledObject.Instance.active == false)
            {
                pooledObject.Release();
                return true;
            }
            return false;
        }));
    }

    public void Shoot()
    {
        if (ammoComp.UseAmmo(m_ammoUsedOnShot) == 0)
        {
            Debug.Log("Out of ammo");
            return;
        };

        Vector3 origin = m_parentCamera.transform.position;

        Vector3 fwd = m_parentCamera.transform.forward;

        // Calculate an origin to launch the projectile from that 
        // doesnt collide with the character. 
        Vector3 fwd_cpy = fwd.normalized;
        fwd_cpy *= (m_parentCollider.height / 2 + (m_projectileCollider.radius / 2));
        origin += fwd_cpy;

        // Acquire a pooled projectile and add it to tracking list.
        var pooled = m_poolManager.Request(m_projectilePrefab);
        m_shotProjectiles.Add(pooled);

        var projectile = pooled.Instance;
        projectile.transform.rotation = m_parentCamera.rotation;
        projectile.transform.position = origin;

        //Vector3 projectile_vel = m_fInherentProjectileVel * fwd;// + transform.parent.GetComponent<Rigidbody>().velocity);
        Vector3 projectile_vel = fwd * m_fInherentProjectileVel; // + transform.root.gameObject.GetComponent<Rigidbody>().velocity;


        projectile.GetComponent<Rigidbody>().velocity = projectile_vel;
        projectile.GetComponent<IDamageSender>().SourceWeapon = gameObject;
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

    public void GravitySwitch(Vector3 grav)
    {
        foreach (PooledGameObject go in m_shotProjectiles)
        {
            go.Instance.GetComponent<GravityProjectile>().GravitySwitch(grav);
        }
    }
}
