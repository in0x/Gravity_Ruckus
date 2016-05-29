using UnityEngine;

public class AmmoPickup : ActionOnPickUpComponent
{
    public GameObject m_weaponPrefab;
    public int m_ammoCount;

    protected override void DoAction(Collider collider)
    {
        // Find weapon prefab in players children and try to give ammo
        foreach (Transform child in collider.transform.root)
        {
            if (child.gameObject.name == m_weaponPrefab.name)
            {
                bool pickedUp = child.gameObject.GetComponent<AmmoComponent>().Refill(m_ammoCount);

                if (pickedUp)
                {
                    m_pickUpController.AddDeactivated(this);
                    gameObject.SetActive(false);
                }
            }
        };
    }
}
