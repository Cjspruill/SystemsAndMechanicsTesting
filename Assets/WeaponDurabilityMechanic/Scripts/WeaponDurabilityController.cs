using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDurabilityController : MonoBehaviour
{
    [SerializeField] GameObject swordObjectToManipulate; //Our current weapon in our hands
    [SerializeField] GameObject currentWeaponModel; //
    [SerializeField] GameObject goodWeapon; //On in hands at start
    [SerializeField] GameObject medWeapon; //Turned off in hand
    [SerializeField] GameObject badWeapon; //Turned off in hand
    [SerializeField] int durability; //Weapon durabiliy

    [SerializeField] Transform originalLocation;
    [SerializeField] Transform endSwingLocation;
    [SerializeField] Quaternion originalRotation;
    [SerializeField] Quaternion endSwingRotation;
    [SerializeField] float swingSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool isSwinging;
    [SerializeField] bool canSwing;

    public int Durability { get => durability; set => durability = value; }
    public bool IsSwinging { get => isSwinging; set => isSwinging = value; }

    private void Start()
    {
        currentWeaponModel = goodWeapon;

        originalRotation = originalLocation.rotation;
        endSwingRotation = endSwingLocation.rotation;
        canSwing = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing)
        {
            IsSwinging = true;
            canSwing = false;
        }

        if (IsSwinging)
        {
            Vector3 newPos = Vector3.MoveTowards(swordObjectToManipulate.transform.position, endSwingLocation.position, swingSpeed * Time.deltaTime);
            swordObjectToManipulate.transform.position = newPos;

            Quaternion newRot = Quaternion.Slerp(swordObjectToManipulate.transform.rotation, endSwingRotation, rotationSpeed * Time.deltaTime);
            swordObjectToManipulate.transform.rotation = newRot;

            if(swordObjectToManipulate.transform.position == endSwingLocation.position)
            {
                IsSwinging = false;
            }
        }
        else if(!isSwinging && !canSwing)
        {
            Vector3 newPos = Vector3.MoveTowards(swordObjectToManipulate.transform.position, originalLocation.position, swingSpeed * Time.deltaTime);
            swordObjectToManipulate.transform.position = newPos;

            Quaternion newRot = Quaternion.Slerp(swordObjectToManipulate.transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
            swordObjectToManipulate.transform.rotation = newRot;

            if (swordObjectToManipulate.transform.position == originalLocation.position)
            {
                canSwing = true;
            }
        }
    }

   public void ReduceDurability(int value)
    {
        Durability -= value;


        if(durability <= 0)
        {
            Destroy(currentWeaponModel);
        }

        else if (Durability <= 45)
        {
            currentWeaponModel.SetActive(false);
            currentWeaponModel = badWeapon;
            currentWeaponModel.SetActive(true);
        }
       else if (Durability <= 75)
        {
           currentWeaponModel.SetActive(false);
            currentWeaponModel = medWeapon;
            currentWeaponModel.SetActive(true);
        }
        
    }
}
