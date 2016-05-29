using UnityEngine;
using System.Collections.Generic;

/*
    This is literally a copy-paste of Projectile shoot. The reason for this is 
    to be able to disable the weapon after shootin. I am only using this horrendus 
    "fix" because we've run completely out of time, so a proper implementation
    isnt really possible anymore. 
*/

public class UltimateWeapon : MonoBehaviour, ICanShoot
{
    // Prefab of the projectile to be launched.
    public GameObject m_projectilePrefab;
    public float m_fInherentProjectileVel = 300;
    public int m_ammoUsedOnShot = 1;

    [SerializeField]
    float m_cooldown = 0.5f;

    bool m_available;

    Transform m_parentCamera;
    CapsuleCollider m_parentCollider;
    SphereCollider m_projectileCollider;
    ShootOnClick m_playerShootScript;

    // List to keep track of owned projectiles.
    List<PooledGameObject> m_shotProjectiles;
    ObjectPoolManager m_poolManager;

    // Used to track ammo of weapon
    AmmoComponent m_ammoComp;

    public float Cooldown
    {
        get { return m_cooldown; }
        set { }
    }

    public bool Available
    {
        get { return m_available; }
        set { m_available = value; }
    }

    void Start()
    {
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") m_parentCamera = child;
        }

        m_parentCollider = transform.parent.GetComponent<CapsuleCollider>();
        m_projectileCollider = m_projectilePrefab.GetComponent<SphereCollider>();
        m_poolManager = ObjectPoolManager.Get();
        m_shotProjectiles = new List<PooledGameObject>();

        m_ammoComp = GetComponent<AmmoComponent>();
        m_playerShootScript = transform.root.gameObject.GetComponent<ShootOnClick>();
    }

    void Update()
    {
        m_shotProjectiles.RemoveAll((pooledObject =>
        {
            if (pooledObject.Instance.activeSelf == false)
            {
                pooledObject.Release();
                return true;
            }
            return false;
        }));
    }

    public void Shoot()
    {
        if (m_ammoComp.UseAmmo(m_ammoUsedOnShot) == 0)
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

        var pooled = m_poolManager.Request(m_projectilePrefab);
        m_shotProjectiles.Add(pooled);

        var projectile = pooled.Instance;
        projectile.transform.position = origin;
        projectile.transform.rotation = m_parentCamera.rotation;

        Vector3 projectile_vel = fwd * m_fInherentProjectileVel;

        projectile.GetComponent<Rigidbody>().velocity = projectile_vel;
        projectile.GetComponent<IDamageSender>().SourceWeapon = gameObject;

        // Again, a horrible fix we have to roll because we ran out of time
        m_playerShootScript.IterateWeapons(true);
        gameObject.SetActive(false);
        m_available = false;
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
        m_ammoComp.RefillComplete();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
