using UnityEngine;
using System.Collections;

public class WeaponPickup : ActionOnPickUpComponent
{
    public GameObject weaponPrefab;

    protected override void DoAction(Collider collider)
    {
        Debug.Log("Collision");

        // Find weapon prefab in players children and try to give ammo
        foreach (Transform child in collider.transform.root)
        {
            if (child.gameObject.name == weaponPrefab.name)
            {
                Debug.Log("Found: " + weaponPrefab.name);

                var weapon = child.gameObject.GetComponent<ICanShoot>();

                if (weapon.Available) return;

                weapon.Available = true;
                pickUpController.AddDeactivated(this);
                gameObject.SetActive(false);

                return;
            }
        };
    }
}
