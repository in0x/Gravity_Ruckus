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
    List<HealOnCollision> deactivated;

    void Start()
    {
        deactivated = new List<HealOnCollision>();

        // Add reference to self to each pickup so they can notify the controller when they have been
        // picked up.
        foreach (HealOnCollision healthPickup in pickUpGo.GetComponentsInChildren<HealOnCollision>())
        {
            healthPickup.pickUpController = this;
        }
    }

    void Update()
    {
        for (int idx = 0; idx < deactivated.Count; idx++)
        {
            if (deactivated[idx].TryReactivate()) deactivated.Remove(deactivated[idx]);
        }
    }

    public void AddDeactivated(HealOnCollision pickUp)
    {
        deactivated.Add(pickUp);
    }
}
