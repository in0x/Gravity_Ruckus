using UnityEngine;
using UnityEngine.UI;

public class WriteHealthToText : MonoBehaviour
{
    public GameObject m_player;
    public Text m_text;

    ShootOnClick m_weaponController;
    HealthController m_health;
    string m_playerName;

	void Start()
    {
        m_health = m_player.GetComponent<HealthController>();
        m_playerName = m_player.name;

        m_weaponController = m_player.GetComponent<ShootOnClick>();
	}
	void Update()
    {
        int curAmmo, maxAmmo;
        m_weaponController.GetCurrentAmmoCount(out curAmmo, out maxAmmo);

        m_text.text = "HP: " + m_health.Health.ToString() + " Ammo: " + curAmmo + "/" + maxAmmo;
	}
}
