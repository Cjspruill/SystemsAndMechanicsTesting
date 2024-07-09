using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVersion3 : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;    //Our arrow prefab gameobject
    [SerializeField] Transform arrowLocation;   //The location of where we will place our arrows
    [SerializeField] float arrowThrowForce; //How hard do we throw these arrows?
    [SerializeField] int arrowCount;    //How many arrows do we have?
    [SerializeField] GameObject newArrow;   //New arrow reference, used to do things with the arrow before it is fired.
    [SerializeField] float arrowReloadSpeed;    //How fast can we reload?
    public int ArrowCount { get => arrowCount; set => arrowCount = value; } //Property for arrow count
    public GameObject NewArrow { get => newArrow; set => newArrow = value; } //Property for new arrow
    [SerializeField] Animator bowAnimator; //Animator for the bow object
    [SerializeField] GameObject dummyArrow; //A reference to an arrow we dont actually shoot. Just animate it
    [SerializeField] bool isReady;
    [SerializeField] AudioSource bowAudioSource;
    [SerializeField] AudioClip bowDrawAudioClip;
    [SerializeField] AudioClip bowUnDrawAudioClip;
    [SerializeField] AudioClip bowFireAudioClip;
    [SerializeField] AudioClip arrowNockAudioClip;

    [SerializeField] bool playOnce;
    // Start is called before the first frame update
    void Start()
    {
        //If arrowcount is greater than 0, place a new arrow
        if (ArrowCount > 0)
            PlaceNewArrow();
    }
    // Update is called once per frame
    void Update()
    {
        //If we hold down right mouse button, ready our bow
        if (Input.GetKey(KeyCode.Mouse1) && dummyArrow.activeInHierarchy)
        {
            isReady = true;
            if (!bowAudioSource.isPlaying && !playOnce)
            {
                bowAudioSource.clip = bowDrawAudioClip;
                bowAudioSource.pitch = 1;
                bowAudioSource.Play();
                playOnce = true;
            }
            bowAnimator.SetBool("IsReady", isReady);   
        }
        //If we release right mouse button, unready our bow
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            playOnce = false;

            isReady = false;
            if (!bowAudioSource.isPlaying && !playOnce && dummyArrow.activeInHierarchy)
            {
                bowAudioSource.clip = bowUnDrawAudioClip;
                bowAudioSource.pitch = 1;
                bowAudioSource.Play();
                playOnce = true;
            }
            bowAnimator.SetBool("IsReady", isReady);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|BowDownIdle") && !dummyArrow.activeInHierarchy)
            {
                //If we still have arrows, place a new arrow, wait for reload speed
                if (ArrowCount > 0)
                    Invoke("PlaceNewArrow", arrowReloadSpeed);
            }
        }


        if (isReady)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && dummyArrow.activeInHierarchy)
            {
                FireArrow();
            }
        }
        else
        {
            playOnce = false;
        }
    }

    //Places a new arrow
    public void PlaceNewArrow()
    {
        bowAudioSource.clip = arrowNockAudioClip;
        bowAudioSource.pitch = 1;
        bowAudioSource.Play();
        //Decrement arrow count
        ArrowCount--;

        dummyArrow.SetActive(true);
    }

    //Fires our currently loaded arrow
    void FireArrow()
    {
        bowAudioSource.clip = bowFireAudioClip;
        bowAudioSource.pitch = Random.Range(.8f, 1);
        bowAudioSource.Play();

        dummyArrow.SetActive(false);

        //Create a new arrow and parent it to the arrow location
        NewArrow = Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation);
        NewArrow.transform.parent = arrowLocation;

        //Turn on the boxcollidertrigger of the arrow, and set the rigidbody iskinematic to isfalse

        NewArrow.GetComponent<Rigidbody>().isKinematic = false;

        //Add force to the rigidbody, set the parent to null
        NewArrow.GetComponent<Rigidbody>().AddForce(arrowLocation.right * arrowThrowForce, ForceMode.Impulse);
        NewArrow.transform.parent = null;
        Invoke("EnableBoxCollider", .2f);
    }
    void EnableBoxCollider()
    {
        NewArrow.GetComponent<Arrow>().boxColliderTrigger.enabled = true;
        NewArrow = null;
    }
}
