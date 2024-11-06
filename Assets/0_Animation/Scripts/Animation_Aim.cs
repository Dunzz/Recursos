using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Aim : MonoBehaviour
{
    public LayerMask hitLayers;      
    public GameObject objectToMove;
    public Transform origen;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(origen.position, origen.forward, out hit, Mathf.Infinity, hitLayers))
        {
            objectToMove.transform.position = hit.point;
        }
    }
}
