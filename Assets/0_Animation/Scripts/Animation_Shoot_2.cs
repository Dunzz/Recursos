using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem.XR;

public class Animation_Shoot_2 : MonoBehaviour
{
    public float rayDistance = 1000f;     
    public LayerMask targetLayers;       
    public GameObject impactEffect;      
    public int damage = 10;              
    public float fireRate = 0.2f;        
    public Camera camera;
    public float shakeTime = 0.3f;
    public float shakeMagnitude = 0.03f;

    private float nextTimeToFire = 0f;   
    public float normalFOV = 60f;
    public float shootFOV = 25;
    public float runFOV = 75;
    public float timeFOV = 5;
    private bool isZoomingIn = false;  
    private bool isZoomingOut = false;  

    private Animation_States states;
    public Animator weapon;

    private void Start()
    {
        states = GetComponent<Animation_States>();
    }

    void Update()
    {
        if (!states.isOnRun)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                isZoomingIn = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                isZoomingIn = false;
            }

            if (isZoomingIn)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, shootFOV, Time.deltaTime * timeFOV);
            }
            else
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, normalFOV, Time.deltaTime * timeFOV);
            }
        }

        if (states.isOnRun)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isZoomingOut = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isZoomingOut = false;
            }

            if (isZoomingOut)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, runFOV, Time.deltaTime * timeFOV);
            }
            else
            {   
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, normalFOV, Time.deltaTime * timeFOV);
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Se dispara");
        weapon.GetComponent<AudioSource>().PlayOneShot(weapon.GetComponent<AudioSource>().clip);
        StartCoroutine(camera.GetComponent<Animation_CameraShake_2>().Shake(shakeTime, shakeMagnitude));
        weapon.Play("Weapon_Shot");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayers))
        {
            Animation_Target target = hit.collider.GetComponent<Animation_Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
