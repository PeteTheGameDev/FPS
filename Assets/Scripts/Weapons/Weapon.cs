using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Checklist:
    /// 1) Utilize Unity events for things such as Fire, Reload, etc.
    /// 2) Create a scriptable object script to store data for various weapons
    /// 3) Use Raycasts for Hit-Scan bullet damage
    /// </summary>

    public void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        

        fire.AddListener(FireWeapon);
    }

    public virtual void Update()
    {
        

        
    }

    public virtual void FixedUpdate()
    {
        WeaponEvents();
    }

    public void FireWeapon()
    {
        Debug.Log("Firing weapon");
    }

    private void WeaponEvents()
    {
        if (inputManager.fireHeld && weaponData.isFullAuto)
        {
            FireWeapon();
        }
    }

    // Components
    private InputManager inputManager;
    public WeaponData weaponData;

    // Events
    public UnityEvent fire;
}
