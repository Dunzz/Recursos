using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_FollowTarget : MonoBehaviour
{
    public Transform target; // Objeto objetivo a seguir
    public bool followPosition = true; // Seguir posición
    public bool followRotation = true; // Seguir rotación

    void Update()
    {
        if (target != null)
        {
            // Seguir la posición si está activado
            if (followPosition)
            {
                transform.position = target.position;
            }

            // Seguir la rotación si está activado
            if (followRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
