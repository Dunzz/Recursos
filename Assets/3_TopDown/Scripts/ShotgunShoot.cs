using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    public Transform firePoint; // El punto desde donde se disparar�n los proyectiles
    public GameObject bulletPrefab; // Prefab del proyectil (balas o part�culas)
    public float bulletForce = 20f; // Fuerza con la que los proyectiles son disparados
    public int pelletCount = 10; // Cantidad de perdigones disparados
    public float spreadAngle = 15f; // �ngulo de dispersi�n de los perdigones
    public float fireRate = 1f; // Tiempo entre disparos
    private float nextFireTime = 0f; // Control de tiempo entre disparos

    void Update()
    {
        // Disparar cuando se presiona el bot�n de disparo y el tiempo de espera ha pasado
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Generar la rotaci�n aleatoria para simular la dispersi�n (spread)
            Quaternion spreadRotation = Quaternion.Euler(
                firePoint.rotation.eulerAngles.x + Random.Range(-spreadAngle, spreadAngle),
                firePoint.rotation.eulerAngles.y + Random.Range(-spreadAngle, spreadAngle),
                firePoint.rotation.eulerAngles.z
            );

            // Crear el proyectil en el punto de disparo
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);

            // Obtener el componente Rigidbody del proyectil
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplicar fuerza al proyectil en la direcci�n del "spreadRotation" calculado
                rb.AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
            }
        }
    }
}
