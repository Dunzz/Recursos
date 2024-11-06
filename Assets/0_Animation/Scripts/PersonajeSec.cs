using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonajeSec : MonoBehaviour
{
    public Animator personajeSec;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            personajeSec.SetTrigger("Parado");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            personajeSec.SetTrigger("Sentado");
        }
    }
}
