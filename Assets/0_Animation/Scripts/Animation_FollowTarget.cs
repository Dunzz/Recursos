using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_FollowTarget : MonoBehaviour
{
    public Transform target; // Objeto objetivo a seguir
    public bool followPosition = true; // Seguir posici�n
    public bool followRotation = true; // Seguir rotaci�n

    void Update()
    {
        if (target != null)
        {
            // Seguir la posici�n si est� activado
            if (followPosition)
            {
                transform.position = target.position;
            }

            // Seguir la rotaci�n si est� activado
            if (followRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
