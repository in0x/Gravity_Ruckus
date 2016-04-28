using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GravityLauncher : MonoBehaviour, ICanShoot, IGravityObserver
{
    // Prefab of the projectile to be launched.
    public GameObject m_projectilePrefab;
    public float m_fInherentProjectileVel = 300;

    [SerializeField]
    float m_cooldown = 0.5f;

    Transform m_parentCamera;
    CapsuleCollider m_parentCollider;
    private SphereCollider m_projectileCollider;

    // List to keep track of owned projectiles.
    List<PooledGameObject> m_shotProjectiles = new List<PooledGameObject>();
    ObjectPoolManager m_poolManager;

    public float Cooldown
    {
        get { return m_cooldown; }
        set { }
    }

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
        projectile.transform.position = origin;
        projectile.transform.rotation = m_parentCamera.rotation;

        Vector3 projectile_vel = fwd * m_fInherentProjectileVel; //+ transform.parent.GetComponent<Rigidbody>().velocity;

        projectile.GetComponent<Rigidbody>().velocity = projectile_vel;
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void GravitySwitch(Vector3 grav)
    {
        foreach (PooledGameObject go in m_shotProjectiles)
        {
            go.Instance.GetComponent<GravityProjectile>().GravitySwitch(grav);
        }
    }
}
