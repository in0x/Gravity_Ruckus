using UnityEngine;
using System.Collections;

public class ProjectileShoot : MonoBehaviour, ICanShoot
{
    public GameObject projectilePrefab;
    public float m_fInherentProjectileVel = 300;

    Transform parentCamera;
    CapsuleCollider parentCollider;
    SphereCollider projectileCollider;

    void Start ()
    {
        // Find the transform of the parents camera component
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "MainCamera") parentCamera = child;
        }

        parentCollider = transform.parent.GetComponent<CapsuleCollider>();

        if (parentCollider != null) Debug.Log("Found");

        projectileCollider = projectilePrefab.GetComponent<SphereCollider>();    
    }

    public void shoot()
    {
        Vector3 origin = parentCamera.transform.position;
        
        Vector3 fwd = parentCamera.transform.forward;

        Vector3 fwd_cpy = fwd.normalized;
        fwd_cpy *= (parentCollider.height / 2 + (projectileCollider.radius / 2));
        origin += fwd_cpy;

        GameObject projectile = (GameObject)Instantiate(projectilePrefab, origin, parentCamera.rotation);

        Vector3 projectile_vel = fwd * m_fInherentProjectileVel + transform.parent.GetComponent<Rigidbody>().velocity;

        projectile.GetComponent<Rigidbody>().velocity = projectile_vel;
    }
    public void enable()
    {
        gameObject.SetActive(true);
    }
    public void disable()
    {
        gameObject.SetActive(false);
    }
}
