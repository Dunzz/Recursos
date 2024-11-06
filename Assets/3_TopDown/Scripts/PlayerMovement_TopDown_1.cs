using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_TopDown_1 : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f; // Velocidad de movimiento
    public float gravity = -9.81f; // Valor de la gravedad
    public float jumpHeight = 2f; // Altura del salto
    public Camera mainCamera; // Cámara principal para el control del movimiento y rotación

    private Vector3 velocity; // Control de la velocidad de caída
    private bool isGrounded; // Para verificar si el personaje está en el suelo
    public Transform groundCheck; // Objeto vacío para verificar el suelo
    public float groundDistance = 0.4f; // Radio de la esfera para detectar el suelo
    public LayerMask groundMask; // Capa del suelo

    void Update()
    {
        // Verificar si estamos en el suelo usando una esfera en la posición groundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resetear la velocidad vertical si estamos en el suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Lo dejamos en un valor bajo en lugar de 0 para evitar bugs en pendientes
        }

        // Movimiento basado en la cámara y WASD
        MovePlayerWithCamera();

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicar gravedad manualmente
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotar hacia donde esté el ratón
        RotateTowardsMouse();
    }

    void MovePlayerWithCamera()
    {
        // Obtener los ejes de entrada de WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcular la dirección en la que la cámara está mirando, ignorando el eje Y
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        // Asegurarnos de que la dirección esté en el plano XZ (ignorando la altura)
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Movimiento en dirección a donde está mirando la cámara
        Vector3 move = forward * z + right * x;
        controller.Move(move * speed * Time.deltaTime);
    }

    void RotateTowardsMouse()
    {
        // Obtener la posición del ratón en el mundo
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 direction = (pointToLook - transform.position).normalized;
            direction.y = 0; // Mantener solo la rotación en el plano XZ

            // Rotar el jugador para que mire hacia el punto donde está el ratón
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
