﻿using UnityEngine;
using System.Collections.Generic;

public class BouncePistol : MonoBehaviour, ICanShoot
{
    // Prefab of the projectile to be shoot.
    public GameObject m_projectilePrefab;
    public float m_fInherentProjectileVel = 300;
    public float m_cooldown = 0.5f;
    public int m_ammoUsedOnShot = 1;

    float m_projectileSpeedMul;
    bool m_available;

    Transform m_parentCamera;
    CapsuleCollider m_parentCollider;
    CapsuleCollider m_projectileCollider;

    // List to keep track of owned projectiles.
    List<PooledGameObject> m_shotProjectiles = new List<PooledGameObject>();
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
        get { return true; }
        set { m_available = value; }
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
        m_projectileSpeedMul = m_projectilePrefab.GetComponent<BounceProjectile>().m_speed;
        m_ammoComp = GetComponent<AmmoComponent>();
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

    public void GetAmmoState(out int currentAmmo, out int maxAmmo)
    {
        var ammoComp = GetComponent<AmmoComponent>();
        maxAmmo = ammoComp.m_maxAmmo;
        currentAmmo = ammoComp.CurrentAmmo;
    }

    public void Shoot()
    {
        if (m_ammoComp.UseAmmo(m_ammoUsedOnShot) == 0) return;
        
        Vector3 origin = m_parentCamera.transform.position;
        Vector3 fwd = m_parentCamera.transform.forward;

        // Calculate an origin to launch the projectile from that 
        // doesnt collide with the character. 
        Vector3 fwd_cpy = fwd.normalized;
        fwd_cpy *= (m_parentCollider.height / 2 + (m_projectileCollider.radius / 2));
        origin += fwd_cpy;
  
        // Acquire 9 projectiles and shoot them in a grid
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                var pooled = m_poolManager.Request(m_projectilePrefab);
                m_shotProjectiles.Add(pooled);              
                pooled.Instance.GetComponent<IDamageSender>().SourceWeapon = gameObject;

                // This may be a big slowdown and should be optimised later.
                Rigidbody instanceRB = pooled.Instance.GetComponent<Rigidbody>();
                instanceRB.velocity = fwd * m_fInherentProjectileVel * m_projectileSpeedMul;

                pooled.Instance.transform.rotation = Quaternion.Euler(90, 0, 0) * Quaternion.LookRotation(instanceRB.velocity);
                pooled.Instance.transform.position = origin + new Vector3(x * m_projectileCollider.radius * 3.5f, y * m_projectileCollider.radius * 3.5f, 0);
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
