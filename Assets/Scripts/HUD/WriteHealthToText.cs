using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteHealthToText : MonoBehaviour
{
    public GameObject player;
    public Text text;

    ShootOnClick weaponController;
    HealthController health;
    string playerName;

	void Start()
    {
        health = player.GetComponent<HealthController>();
        playerName = player.name;

        weaponController = player.GetComponent<ShootOnClick>();
	}
	void Update()
    {
        int curAmmo, maxAmmo;
        weaponController.GetCurrentAmmoCount(out curAmmo, out maxAmmo);

        text.text = "HP: " + health.Health.ToString() + " Ammo: " + curAmmo + "/" + maxAmmo;
	}
}
