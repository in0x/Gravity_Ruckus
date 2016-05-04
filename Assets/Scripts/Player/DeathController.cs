using UnityEngine;
using System.Collections;

/*\
|*| This class is responsible for organising player deaths and respawns.
|*| It deactivates and activates the appropriate components sets all 
|*| required attributes, such as position and gravity on respawn. 
\*/
public class DeathController : MonoBehaviour
{
    GravityHandler m_gravHandler;
    HealthController m_health;
    Rigidbody m_body;
    ShootOnClick m_weapons;

    // Set by ScoreController on Startup
    public ScoreController m_scoreController;

    PlayerSpawnController m_spawnController;

    string lastWeaponUsed;

    void Start()
    {
        m_spawnController = FindObjectOfType<PlayerSpawnController>();
        m_gravHandler = GetComponent<GravityHandler>();
        m_health = GetComponent<HealthController>();
        m_body = GetComponent<Rigidbody>();
        m_weapons = GetComponent<ShootOnClick>();
    }
    
    public void Respawn(Vector3 position, Quaternion rotation, Vector3 gravity)
    {
        foreach (Transform tf in gameObject.transform)
        {
            GameObject child = tf.gameObject;

            if (child.tag == "Weapon")
            {
                // Skip all other weapons, otherwise every weapon would be active
                if (child.name != lastWeaponUsed) continue;
            }

            child.SetActive(true);
        }

        m_gravHandler.Gravity = gravity;
        m_health.Refill();

        transform.position = position;
        transform.rotation = rotation;

        m_body.velocity = Vector3.zero;
        m_body.angularVelocity = Vector3.zero;
    }

    public void Die(DamageInfo damageKilledPlayer)
    {
        Debug.Log("Killed by: " + damageKilledPlayer.senderName + " with: " + damageKilledPlayer.sourceName);

        foreach (Transform tf in gameObject.transform)
        {
            GameObject child = tf.gameObject;

            if (child.tag == "Weapon")
            {
                if (child.active) lastWeaponUsed = child.name;
            }

            child.SetActive(false);                     
        }

        m_weapons.DisableAllWeapons();
       
        m_spawnController.RegisterAsDead(this.gameObject);
        m_scoreController.ProcessKill(gameObject, damageKilledPlayer);
    }
}
