using UnityEngine;

public class ActionOnPickUpComponent : MonoBehaviour
{
    public PickupController m_pickUpController;
    public float m_cooldown = 5f;

    float m_timeSincePick = 0;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.tag == "Player") DoAction(collider);
    }

    // Override this function to implement custom behaviour
    protected virtual void DoAction(Collider collider) {}
    
    public bool TryReactivate()
    {
        m_timeSincePick += Time.deltaTime;

        if (m_timeSincePick >= m_cooldown)
        {
            gameObject.SetActive(true);
            return true;
        }
        return false;
    }
}
