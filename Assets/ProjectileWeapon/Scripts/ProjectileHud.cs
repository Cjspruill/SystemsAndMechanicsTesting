using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileHud : MonoBehaviour
{
    [SerializeField] ProjectileWeapon projectileWeapon; //Projectile weapon reference
    [SerializeField] TextMeshProUGUI ammoText; //Ammo text reference

    // Update is called once per frame
    void Update()
    {
        if (projectileWeapon != null)
        {
            ammoText.text = "Ammo: " + projectileWeapon.Ammo.ToString();
        }
    }
}
