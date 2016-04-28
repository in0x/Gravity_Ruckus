using UnityEngine;
using System.Collections.Generic;

/*\
|*| Responsible for keeping track of deactivated pickups
|*| and trying to reactivate them (check if they have been
|*| deactivated for longer than their cooldown).
\*/
public class PickupController : MonoBehaviour
{
    public GameObject pickUpGo;
    List<ActionOnPickUpComponent> deactivated;

    void Start()
    {
        deactivated = new List<ActionOnPickUpComponent>();

        // Add reference to self to each pickup so they can notify the controller when they have been
        // picked up.
        foreach (ActionOnPickUpComponent pickup in pickUpGo.GetComponentsInChildren<ActionOnPickUpComponent>())
        {
            pickup.pickUpController = this;
        }
    }

    void Update()
    {
        for (int idx = 0; idx < deactivated.Count; idx++)
        {
            if (deactivated[idx].TryReactivate()) deactivated.Remove(deactivated[idx]);
        }
    }

    public void AddDeactivated(ActionOnPickUpComponent pickUp)
    {
        deactivated.Add(pickUp);
    }
}
