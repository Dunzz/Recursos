using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem.XR;

public class Animation_Shoot : MonoBehaviour
{
    public float rayDistance = 1000f;     // Distancia máxima del disparo
    public LayerMask targetLayers;       // Capas que pueden ser impactadas (enemigos u objetos destructibles)
    public GameObject impactEffect;      // Efecto visual al impactar (opcional)
    public int damage = 10;              // Daño infligido por cada disparo
    public float fireRate = 0.2f;        // Tiempo entre disparos (en segundos)
    public AudioClip shoot;
    public CinemachineVirtualCamera camera;

    private float nextTimeToFire = 0f;   // Momento en el que se puede disparar nuevamente
    public float normalFOV = 40f;
    public float shootFOV = 25;
    public float timeFOV = 5;
    private bool isZoomingIn = false;  // Bandera para la transición de zoom

    public Animation_CameraShake shake;

    void Update()
    {
        // Si se mantiene presionado el botón izquierdo del ratón y ya ha pasado suficiente tiempo desde el último disparo
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;  // Actualiza el tiempo del próximo disparo
            Shoot();  // Llamada al método de disparo
        }

        // Detecta si se ha presionado el botón derecho del ratón
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Clic derecho presionado");
            isZoomingIn = true;  // Inicia el zoom
        }

        // Detecta si se ha soltado el botón derecho del ratón
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Clic derecho soltado");
            isZoomingIn = false; // Vuelve al FOV normal
        }

        // Actualiza el FOV de acuerdo al estado
        if (isZoomingIn)
        {
            // Transición hacia el shootFOV
            camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, shootFOV, Time.deltaTime * timeFOV);
        }
        else
        {
            // Transición hacia el normalFOV
            camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, normalFOV, Time.deltaTime * timeFOV);
        }
    }

    void Shoot()
    {
        Debug.Log("Se dispara");
        AudioSource.PlayClipAtPoint(shoot, this.gameObject.transform.position, 1);
        shake.ShakeCamera(1f, 0.2f);

        // Lanza el raycast desde la cámara hacia la dirección del cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Si el raycast impacta algo dentro de las capas especificadas
        if (Physics.Raycast(ray, out hit, rayDistance, targetLayers))
        {
            // Verifica si el objeto impactado tiene el componente "Target" (por ejemplo, un enemigo)
            Animation_Target target = hit.collider.GetComponent<Animation_Target>();
            if (target != null)
            {
                target.TakeDamage(damage);  // Aplica daño al objetivo
            }

            // Instancia un efecto de impacto en el punto de impacto (opcional)
            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
