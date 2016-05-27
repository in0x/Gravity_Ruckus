using UnityEngine;

public class HealOnCollision : ActionOnPickUpComponent
{
    protected override void DoAction(Collider collider)
    {
        // Using GetComponent here to be able to recieve return value
        // and check if player actually recieved heal.
        bool pickedUp = collider.transform.root.gameObject.GetComponent<HealthController>().Heal(10f);

        if (pickedUp)
        {
            m_pickUpController.AddDeactivated(this);
            gameObject.SetActive(false);
        }
    }   
}
