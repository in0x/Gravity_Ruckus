using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/*\
|*| Base class for projectile weapons. Fulfills the ICanShoot contract,
|*| acquires and releases pooled projectiles and shoots a single projectile.
|*| This class can be extended to implement a custom shooting behaviour.
\*/
public class ProjectileShoot : MonoBehaviour, ICanShoot
{
    // Prefab of the projectile to be launched.
    public GameObject m_projectilePrefab;
    public float m_fInherentProjectileVel = 300;

    protected Transform m_parentCamera;
    protected CapsuleCollider m_parentCollider;
    protected SphereCollider m_projectileCollider;

    protected List<PooledGameObject> m_shotProjectiles; 
    protected ObjectPoolManager m_poolManager;
    // List to keep track of owned projectiles.
    
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

    public virtual void Shoot()
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
}
