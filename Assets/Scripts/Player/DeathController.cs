﻿using UnityEngine;

/*\
|*| This class is responsible for organising player deaths and respawns.
|*| It deactivates and activates the appropriate components sets all 
|*| required attributes, such as position and gravity on respawn. 
\*/
public class DeathController : MonoBehaviour
{
    // Set by ScoreController on Startup
    public ScoreController m_scoreController;
    public GameObject m_deathRoom;

    GravityHandler m_gravHandler;
    HealthController m_health;
    Rigidbody m_body;
    ScreenDamageEffect m_cameraEffect;
    ShootOnClick m_weapons;   
    PlayerSpawnController m_spawnController;
    string m_lastWeaponUsedStr;

    void Start()
    {
        m_spawnController = FindObjectOfType<PlayerSpawnController>();
        m_gravHandler = GetComponent<GravityHandler>();
        m_health = GetComponent<HealthController>();
        m_body = GetComponent<Rigidbody>();
        m_weapons = GetComponent<ShootOnClick>();
        m_cameraEffect = GetComponentInChildren<ScreenDamageEffect>();
    }
    
    public void Respawn(Vector3 position, Quaternion rotation, Vector3 gravity)
    {
        foreach (Transform tf in gameObject.transform)
        {
            GameObject child = tf.gameObject;
            
            if (child.tag == "Weapon" && child.name != "BouncePistol")
            {
                continue;
            }

            child.SetActive(true);
        }

        m_weapons.ResetWeaponsRespawn();
        m_cameraEffect.ToggleBlack();

        m_gravHandler.Gravity = gravity;
        m_health.Refill();

        transform.position = position;
        transform.rotation = rotation;

        m_body.velocity = Vector3.zero;
        m_body.angularVelocity = Vector3.zero;
    }

    public void Die(DamageInfo damageKilledPlayer)
    {
        foreach (Transform tf in gameObject.transform)
        {
            GameObject child = tf.gameObject;

            if (child.tag == "Weapon")
            {
                if (child.activeSelf) m_lastWeaponUsedStr = child.name;
            }
            else if (child.tag == "MainCamera" || child.tag == "Model") 
            {
                continue;
            }
            child.SetActive(false);
        }

        m_weapons.DisableAllWeapons();
        m_spawnController.RegisterAsDead(gameObject);
        m_scoreController.ProcessKill(gameObject, damageKilledPlayer);
        transform.position = new Vector3(-3055, 1172, -600);

        m_body.velocity = Vector3.zero;
        m_body.angularVelocity = Vector3.zero;

        m_cameraEffect.ToggleBlack();
    }
}
