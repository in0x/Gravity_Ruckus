using UnityEngine;
public class KillPlayerOnTouch : MonoBehaviour
{
    // Used to not trigger death twice on player,
    // since both legs touch the floor and trigger a collision.
    GameObject lastCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (lastCollision == collision.collider.gameObject.transform.parent.gameObject)
        {
            return;
        }
        else lastCollision = collision.collider.gameObject.transform.parent.gameObject; 

        collision.collider.SendMessageUpwards("Die", new DamageInfo(gameObject, 0f, false, true), SendMessageOptions.DontRequireReceiver);
    }    
}
