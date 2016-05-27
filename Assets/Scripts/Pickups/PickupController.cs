using UnityEngine;
using System.Collections.Generic;

/*\
|*| Responsible for keeping track of deactivated pickups
|*| and trying to reactivate them (check if they have been
|*| deactivated for longer than their cooldown).
\*/
public class PickupController : MonoBehaviour
{
    public GameObject m_pickUpGo;
    List<ActionOnPickUpComponent> m_deactivated;

    void Start()
    {
        m_deactivated = new List<ActionOnPickUpComponent>();

        // Add reference to self to each pickup so they can notify the controller when they have been
        // picked up.
        foreach (ActionOnPickUpComponent pickup in m_pickUpGo.GetComponentsInChildren<ActionOnPickUpComponent>())
        {
            pickup.m_pickUpController = this;
        }
    }

    void Update()
    {
        for (int idx = 0; idx < m_deactivated.Count; idx++)
        {
            if (m_deactivated[idx].TryReactivate()) m_deactivated.Remove(m_deactivated[idx]);
        }
    }

    public void AddDeactivated(ActionOnPickUpComponent pickUp)
    {
        m_deactivated.Add(pickUp);
    }
}
