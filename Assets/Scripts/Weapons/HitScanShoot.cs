using UnityEngine;
using System.Collections;
        
public class HitScanShoot : MonoBehaviour, ICanShoot
{
    public float m_fRange = 0;
    public float m_fTimeToDrawRay = 0;

    float m_TimeSinceShot = 0;

    LineRenderer rayRenderer;
    Transform parentCamera;

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

    public void shoot()
    {
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

            // Careful, this is expensive as it uses reflection
            collisionInfo.collider.gameObject.SendMessage("RecieveDamage", 50f, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // If no hit, draw the point until max range
            rayRenderer.SetPosition(1, ray.GetPoint(m_fRange));
        }

        rayRenderer.enabled = true;
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
