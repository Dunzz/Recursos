using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Plataforma : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTRAAAAAAAAAAAAAAA");
            other.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
