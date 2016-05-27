using UnityEngine;
public class KillPlayerOnTouch : MonoBehaviour
{
    // Used to not trigger death twice on player,
    // since both legs touch the floor and trigger a collision.
    GameObject m_lastCollision;

    void Update()
    {
        // Let's hope Update doesnt run between two OnCollisionEnters.
        m_lastCollision = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (m_lastCollision == collision.collider.gameObject.transform.parent.gameObject)
        {
            return;
        }
        else m_lastCollision = collision.collider.gameObject.transform.parent.gameObject; 

        collision.collider.SendMessageUpwards("Die", new DamageInfo(gameObject, 0f, false, true), SendMessageOptions.DontRequireReceiver);
    }    
}
