using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShootOnClick : MonoBehaviour, IInputObserver
{
    public PlayerInput PlayerInputRef { get; set; }
    
    List<ICanShoot> m_weapons;
    CircularListIterator<ICanShoot> currentWeapon;

    bool m_isShooting = false;
    const int left = 0;

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
        
        currentWeapon = new CircularListIterator<ICanShoot>(m_weapons);
        currentWeapon.Current.Enable();
    }

    void Update()
    {
        if (m_isOnCD)
        {
            m_CDtime += Time.deltaTime;
            if (m_CDtime >= currentWeapon.Current.Cooldown)
            {
                m_isOnCD = false;
                m_CDtime = 0;
            }
        }

        if (!m_isOnCD)
        {
            if (PlayerInputRef.GetAxis("Shoot")>0.1)
            {
                m_isShooting = true;
            }
        }

        WeaponSwitch();
	}
    
    void WeaponSwitch()
    {
        if (PlayerInputRef.GetButtonDown("WeaponSwitchPrev"))
        {
            IterateWeapons(false);
        }
        else if (PlayerInputRef.GetButtonDown("WeaponSwitchNext"))
        {
            IterateWeapons(true);
        }
    }

    void IterateWeapons(bool forward)
    {
        var currentWep = currentWeapon.Current;
        
        Action move;
        if (forward)
        {
            move = currentWeapon.MoveNext;
        }
        else move = currentWeapon.MoveBack;
         
        currentWeapon.Current.Disable();
        move();

        while (!currentWeapon.Current.Available && currentWep != currentWeapon.Current)
        {
            move();  
        }     

        currentWeapon.Current.Enable();
    }

    public void DisableAllWeapons()
    {
        foreach (var weapon in m_weapons) weapon.Disable();
    }

    public void GetCurrentAmmoCount(out int currentAmmo, out int maxAmmo)
    {
        currentWeapon.Current.GetAmmoState(out currentAmmo, out maxAmmo);
    }

    void FixedUpdate()
    {
        if (m_isShooting)
        {
            m_isShooting = false;
            
            // Neat.
            (currentWeapon.Current as ICanShoot).Shoot();

            m_isOnCD = true;
        }
    }
}
