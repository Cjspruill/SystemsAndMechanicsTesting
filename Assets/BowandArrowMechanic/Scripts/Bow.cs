using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowLocation;
    [SerializeField] float arrowThrowForce;
    [SerializeField] int arrowCount;

    [SerializeField] GameObject newArrow;

    public int ArrowCount { get => arrowCount; set => arrowCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        if(ArrowCount > 0)
        PlaceNewArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireArrow();
        }
    }

     void PlaceNewArrow()
    {
        ArrowCount--;
        newArrow = Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation);
        newArrow.transform.parent = arrowLocation;

    }

     void FireArrow()
    {
        if (newArrow == null) return;
        newArrow.GetComponent<Arrow>().boxColliderTrigger.enabled = true;
        newArrow.GetComponent<Rigidbody>().isKinematic = false;
        newArrow.GetComponent<Rigidbody>().AddForce(arrowLocation.forward * arrowThrowForce, ForceMode.Impulse);
        newArrow.transform.parent = null;
        newArrow = null;

        if (ArrowCount > 0)
            Invoke("PlaceNewArrow", 2f);
    }
}
