using UnityEngine;
using System.Collections;

public class AmmoPickup : ActionOnPickUpComponent
{
    public GameObject prefab;
    public int ammoCount;
    protected override void DoAction(Collider collider)
    {
        // Find weapon prefab in players children and try to give ammo
        foreach (Transform child in collider.transform.root)
        {
            if (child.gameObject.name == prefab.name)
            {
                bool pickedUp = child.gameObject.GetComponent<AmmoComponent>().Refill(ammoCount);

                if (pickedUp)
                {
                    pickUpController.AddDeactivated(this);
                    gameObject.SetActive(false);
                }
            }
        };
    }
}
