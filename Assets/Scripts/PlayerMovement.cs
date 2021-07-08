using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Gravedad.
    private float gravity = 19.62f;
    private float goundDistance = 0.15f;
    private Vector3 velocity;
    public Transform groundCheckTransform;
    public LayerMask groundMask;

    // Salto.
    private float jumpHeight = 2f;
    private bool canJump;

    // Velocidad del personaje.
    private float playerSpeed = 8f;

    // Inputs horizontales (left-right / a-d) y verticales (up-down/w-s).
    private float horizontalInput;
    private float verticalInput;

    // Controlador del personaje.
    private CharacterController characterController;

    // Vectores de movimiento.
    private Vector3 forwardMovement;
    private Vector3 rightMovement;
    private Vector3 movementDirection;


    /**
     * Start is called before the first frame update.
     */
    private void Start() {
        // Obtenemos la referencia al componente CharacterControler del personaje.
        characterController = GetComponent<CharacterController>();
    }

    /**
     * Update is called once per frame.
     */
    private void Update() {
        ReadInputs();
        SetGravity();
        DoMovement();
    }

    /**
     * Se leen los Inputs horizontales y verticales del usuario.
     */
    private void ReadInputs() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        canJump = Input.GetButtonDown("Jump");
    }

    /**
     * Método que controla la gravedad. Solamente si el personaje está tocando el suelo aumentará la velocidad en y.
     * Se multiplica 2 veces por Time.deltaTime porque la grabvedad es una aceleración.
     */
    private void SetGravity() {
        if (!IsGrounded()) {
            velocity.y -= gravity * Time.deltaTime;
        } else if (velocity.y < 0f) {
            velocity.y = 0f;
        }
    }

    /**
     * TRUE si el personaje está tocando el suelo.
     * FALSE si el personaje no está tocando el suelo.
     */
    private bool IsGrounded() {
        return Physics.CheckSphere(groundCheckTransform.position, goundDistance, groundMask);
    }

    /**
     * Se calcula y lleva a cabo el movimiento del personaje.
     */
    private void DoMovement() {
        // Calculamos el movimiento en horizontal y en vertical.
        forwardMovement = transform.forward * verticalInput;
        rightMovement = transform.right * horizontalInput;

        // Calculamos el movimiento total.
        movementDirection = Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f);

        // Si se ha presionado la tecla de salto y el personaje se encuentra en el suelo, se realiza el salto.
        if (canJump && IsGrounded()) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        }

        // Realizamos el movimiento del personaje.
        // Se limita el valor del movimiento a 1 para evitar movimiento en diagonal demasiado rápidos.
        characterController.Move(movementDirection * playerSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }

}
