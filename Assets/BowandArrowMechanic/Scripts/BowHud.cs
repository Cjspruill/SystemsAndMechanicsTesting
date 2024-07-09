using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowHud : MonoBehaviour
{
    [SerializeField] Bow bow; //Reference to the bow for arrowcount;
    [SerializeField] BowVersion2 bowVersion2; //Reference to bow version 2 for arrowcount
    [SerializeField] BowVersion3 bowVersion3; //Reference to bow version 3 for arrowcount
    [SerializeField] TextMeshProUGUI arrowCountText; //Arrow count text reference

    // Update is called once per frame
    void Update()
    {
        //Set arrow count through bow reference
        if(bow!=null)
            arrowCountText.text = "Arrow Count: " + bow.ArrowCount.ToString();
        if (bowVersion2 != null)
            arrowCountText.text = "Arrow Count: " + bowVersion2.ArrowCount.ToString();
        if (bowVersion3 != null)
            arrowCountText.text = "Arrow Count: " + bowVersion3.ArrowCount.ToString();
    }
}
