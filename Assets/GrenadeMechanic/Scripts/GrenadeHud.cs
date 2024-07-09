using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI grenadeText;
    [SerializeField] GrenadeController grenadeController;

    // Update is called once per frame
    void Update()
    {
        grenadeText.text = grenadeController.grenadeType.ToString();
    }
}
