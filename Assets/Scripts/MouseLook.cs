using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script para el control del movimiento de la cámara mediante el ratón.
 */
public class MouseLook : MonoBehaviour {

    // Sensibilidad del ratón.
    private float mouseSensitivity = 200f;

    // Posición del ratón.
    private float mouseX;
    private float mouseY;

    // Valor de la rotación vertical.
    private float rotationX = 0f;

    // Posición del personaje.
    public Transform playerTransform;


    /**
     * Start is called before the first frame update.
     */
    private void Start() {
        // Se establece el cursor bloqueado por defecto.
        Cursor.lockState = CursorLockMode.Locked;
    }

    /**
     * Update is called once per frame.
     */
    private void Update() {
        // Obtenemos la posición del ratón de los ejes X e Y.
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Limitamos la rotación vertical.
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -85f, 85f);

        // Se reposiciona el personaje en función de la posición del ratón.
        // Para la rotación horizontal rotamos al personaje y para la vertical la cámara.
        playerTransform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

}