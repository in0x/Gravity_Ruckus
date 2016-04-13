using UnityEngine;
using System.Collections;

public class HealOnCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.tag == "Player")
        {
            // Using GetComponent here to be able to recieve return value
            // and check if player actually recieved heal.
            bool pickedUp = collider.transform.root.gameObject.GetComponent<HealthController>().Heal(10f);

            if (pickedUp) gameObject.SetActive(false);
        }
    }
	
}
