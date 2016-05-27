using UnityEngine;
using System;
using System.Collections.Generic;

public class ShootOnClick : MonoBehaviour, IInputObserver
{
    public PlayerInput m_playerInputRef { get; set; }
    
    List<ICanShoot> m_weapons;
    CircularListIterator<ICanShoot> m_currentWeapon;

    bool m_isShooting = false;
    const int m_left = 0;

    float m_CDtime = 0;
    bool m_isOnCD = false;

    void Start()
    {
        m_weapons = new List<ICanShoot>();

        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon")
            {
                m_weapons.Add(child.GetComponent<ICanShoot>());
            }
        }

        foreach (var weapon in m_weapons) weapon.Disable();   
        
        m_currentWeapon = new CircularListIterator<ICanShoot>(m_weapons);
        m_currentWeapon.Current.Enable();
    }

    void Update()
    {
        if (m_isOnCD)
        {
            m_CDtime += Time.deltaTime;
            if (m_CDtime >= m_currentWeapon.Current.Cooldown)
            {
                m_isOnCD = false;
                m_CDtime = 0;
            }
        }

        if (!m_isOnCD)
        {
            if (m_playerInputRef.GetAxis("Shoot") > 0.1)
            {
                m_isShooting = true;
            }
        }

        WeaponSwitch();
	}
    
    void WeaponSwitch()
    {
        if (m_playerInputRef.GetButtonDown("WeaponSwitchPrev"))
        {
            IterateWeapons(false);
        }
        else if (m_playerInputRef.GetButtonDown("WeaponSwitchNext"))
        {
            IterateWeapons(true);
        }
    }

    void IterateWeapons(bool forward)
    {
        var currentWep = m_currentWeapon.Current;
        
        Action move;
        if (forward)
        {
            move = m_currentWeapon.MoveNext;
        }
        else move = m_currentWeapon.MoveBack;
         
        m_currentWeapon.Current.Disable();
        move();

        while (!m_currentWeapon.Current.Available && currentWep != m_currentWeapon.Current)
        {
            move();  
        }     

        m_currentWeapon.Current.Enable();
    }

    public void DisableAllWeapons()
    {
        foreach (var weapon in m_weapons)
        {
            weapon.Disable();
            weapon.Available = false;
        }
    }

    public void GetCurrentAmmoCount(out int currentAmmo, out int maxAmmo)
    {
        m_currentWeapon.Current.GetAmmoState(out currentAmmo, out maxAmmo);
    }

    void FixedUpdate()
    {
        if (m_isShooting)
        {
            m_isShooting = false;
            (m_currentWeapon.Current as ICanShoot).Shoot();
            m_isOnCD = true;
        }
    }

    public void ResetWeaponsRespawn()
    {
        while(m_currentWeapon.Current != m_weapons[0])
        {
            m_currentWeapon++;
        }
    }
}
