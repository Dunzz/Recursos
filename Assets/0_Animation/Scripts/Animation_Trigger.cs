using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Trigger : MonoBehaviour
{
    private Animation_Points points;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            points = GameObject.Find("GameManager").GetComponent<Animation_Points>();
            points.Score(1);
            Destroy(gameObject);
        }
    }
}
