using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponDurabilityHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI durablityText;
    [SerializeField] WeaponDurabilityController weaponDurabilityController;

    private void Update()
    {
        durablityText.text = weaponDurabilityController.Durability.ToString();
    }
}
