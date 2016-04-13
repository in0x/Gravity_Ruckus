﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShootOnClick : MonoBehaviour, IInputObserver
{
    public PlayerInput PlayerInputRef { get; set; }

    public float m_fShootCD = 0.5f;

    List<ICanShoot> m_weapons;
    CustomListEnumerator<ICanShoot> currentWeapon;

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
        
        currentWeapon = new CustomListEnumerator<ICanShoot>(m_weapons);
        currentWeapon.Current.enable();
    }

    void Update()
    {
        if (m_isOnCD)
        {
            m_CDtime += Time.deltaTime;
            if (m_CDtime >= m_fShootCD)
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
            currentWeapon.Current.disable();
            currentWeapon.MoveBack();
            currentWeapon.Current.enable();
        }
        else if (PlayerInputRef.GetButtonDown("WeaponSwitchNext"))
        {
            currentWeapon.Current.disable();
            currentWeapon.MoveNext();
            currentWeapon.Current.enable();
        }

        //Debug.Log(GetComponentsInChildren<Collider>().Length);
    }

    void FixedUpdate()
    {
        if (m_isShooting)
        {
            m_isShooting = false;
            
            // Neat.
            (currentWeapon.Current as ICanShoot).shoot();

            m_isOnCD = true;
        }
    }
}
