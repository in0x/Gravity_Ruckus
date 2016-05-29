using UnityEngine;

public class WeaponPickup : ActionOnPickUpComponent
{
    public GameObject m_weaponPrefab;

    protected override void DoAction(Collider collider)
    {
        // Find weapon prefab in players children and try to give ammo
        foreach (Transform child in collider.transform.root)
        {
            if (child.gameObject.name == m_weaponPrefab.name)
            {
                var weapon = child.gameObject.GetComponent<ICanShoot>();

                if (weapon.Available) return;
                weapon.Available = true;
             
                m_pickUpController.AddDeactivated(this);
                gameObject.SetActive(false);

                return;
            }
        };
    }
}
