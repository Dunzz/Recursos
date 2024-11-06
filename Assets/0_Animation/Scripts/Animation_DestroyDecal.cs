using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_DestroyDecal : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 10);
    }
}
