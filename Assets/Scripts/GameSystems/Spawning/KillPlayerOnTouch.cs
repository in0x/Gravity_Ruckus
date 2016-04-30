using UnityEngine;
public class KillPlayerOnTouch : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        collision.collider.SendMessageUpwards("Die");
    }    
}
