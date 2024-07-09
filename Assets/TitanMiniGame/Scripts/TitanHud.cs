using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitanHud : MonoBehaviour
{
    [SerializeField] RotatingTitanCannon rotatingTitanCannon;
    [SerializeField] TextMeshProUGUI cannonBallFirePowerText;
    [SerializeField] TextMeshProUGUI isLoadedText;
    [SerializeField] Slider titanHealthBar;
    [SerializeField] Titan titan;
    // Start is called before the first frame update
    void Start()
    {
        titanHealthBar.maxValue = titan.Health;
    }

    // Update is called once per frame
    void Update()
    {
        cannonBallFirePowerText.text = "FirePower: " + rotatingTitanCannon.CannonBallFirePower.ToString();
        isLoadedText.text = "Is Loaded: " + rotatingTitanCannon.IsLoaded;
        titanHealthBar.value = titan.Health;
    }
}
