using UnityEngine;
using System.Collections;

public class HealOnCollision : MonoBehaviour
{
    public PickupController pickUpController;

    [SerializeField]
    float m_cooldown = 5f;

    float m_timeSincePick = 0;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.tag == "Player")
        {
            // Using GetComponent here to be able to recieve return value
            // and check if player actually recieved heal.
            bool pickedUp = collider.transform.root.gameObject.GetComponent<HealthController>().Heal(10f);

            if (pickedUp)
            {
                pickUpController.AddDeactivated(this);
                gameObject.SetActive(false);
            }
        }
    }
    
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
