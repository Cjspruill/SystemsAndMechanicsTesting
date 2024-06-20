using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVersion2 : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowLocation;
    [SerializeField] float arrowThrowForce;
    [SerializeField] int arrowCount;

    [SerializeField] GameObject newArrow;

    public int ArrowCount { get => arrowCount; set => arrowCount = value; }

    [SerializeField] Transform bowUpPosition;
    [SerializeField] Transform bowDownPosition;
    [SerializeField] float bowTransitionSpeed;
    [SerializeField] bool bowIsReady;
    [SerializeField] GameObject bow;

    // Start is called before the first frame update
    void Start()
    {
        if (ArrowCount > 0)
            PlaceNewArrow();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            ReadyBow();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            UnreadyBow();
        }


        if (bowIsReady)
        {
            Vector3 newPos = Vector3.MoveTowards(bow.transform.position, bowUpPosition.position, bowTransitionSpeed * Time.deltaTime);
            bow.transform.position = newPos;

            if(bow.transform.position == bowUpPosition.position)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    FireArrow();
                }
            }
        }
        else
        {
            Vector3 newPos = Vector3.MoveTowards(bow.transform.position, bowDownPosition.position, bowTransitionSpeed * Time.deltaTime);
            bow.transform.position = newPos;      
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
    void ReadyBow()
    {
        bowIsReady = true;
    }

    void UnreadyBow()
    {
        bowIsReady = false;
    }
}
