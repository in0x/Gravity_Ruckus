using UnityEngine;
using System.Collections.Generic;

public class BouncePistol : MonoBehaviour, ICanShoot
{
    // Prefab of the projectile to be shoot.
    public GameObject m_projectilePrefab;
    public float m_fInherentProjectileVel = 300;

    [SerializeField]
    float m_cooldown = 0.5f;

    Transform m_parentCamera;
    CapsuleCollider m_parentCollider;
    CapsuleCollider m_projectileCollider;

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
        m_projectileCollider = m_projectilePrefab.GetComponent<CapsuleCollider>();
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
   
        // Acquire 9 projectiles and shoot them in a grid
        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                var pooled = m_poolManager.Request(m_projectilePrefab);
                m_shotProjectiles.Add(pooled);
                
                pooled.Instance.transform.position = origin + new Vector3(x * 0.5f, y * 0.5f, 0);
                pooled.Instance.transform.rotation = m_parentCamera.rotation;

                // This may be a big slowdown and should be optimised later.
                pooled.Instance.GetComponent<Rigidbody>().velocity = fwd * m_fInherentProjectileVel;
            }
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
}
