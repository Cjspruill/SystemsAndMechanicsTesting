using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hwacha : MonoBehaviour
{

    [SerializeField] HwachaArrow[] arrows;
    [SerializeField] bool isInTrigger;
    [SerializeField] ParticleSystem sparksParticles;
    [SerializeField] bool alreadyFired;

    private void Start()
    {
        arrows = GetComponentsInChildren<HwachaArrow>();
    }

    private void Update()
    {
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E) && !alreadyFired)
            {
                LightHwacha();
                alreadyFired = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
    void LightHwacha()
    {
        sparksParticles.Play();
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].LightFuse();
        }

        Invoke("StopParticles", 40);
    }

    void StopParticles()
    {
        sparksParticles.Stop();
    }
}
