using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ItemController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("El Player entro el item");
            other.GetComponent<Animation_Player_Actions>().onItem = true;
            other.GetComponent<Animation_Player_Actions>().item = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El Player salio el item");
            other.GetComponent<Animation_Player_Actions>().onItem = false;
            other.GetComponent<Animation_Player_Actions>().item = null;
        }
    }
}
