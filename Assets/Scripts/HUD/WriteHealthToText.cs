using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteHealthToText : MonoBehaviour
{
    public GameObject player;

    ShootOnClick weaponController;
    HealthController health;
    Text text;
    string playerName;

	void Start ()
    {
        health = player.GetComponent<HealthController>();
        playerName = player.name;

        weaponController = player.GetComponent<ShootOnClick>();

        text = GetComponent<Text>();
	}
	void Update ()
    {
        int curAmmo, maxAmmo;
        weaponController.GetCurrentAmmoCount(out curAmmo, out maxAmmo);

        text.text = "HP: " + health.Health.ToString() + " Ammo: " + curAmmo + "/" + maxAmmo;
	}
}
