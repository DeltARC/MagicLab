
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float rollingTorque = 8f;

    [Header("Ground Detection")]
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayers = ~0;

    [Header("Input")]
    [SerializeField] private InputActionReference playerMoveAction;
    [SerializeField] private InputActionReference playerJumpAction;
    [SerializeField] private Transform playerCamera;

    [Header("Sound")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource landingSound;

    private Rigidbody rb;
    private SphereCollider sphereCollider;

    private Vector2 playerMoveAmount;
    private bool jumpRequested;
    private bool isGrounded;

    private int jumpsRemaining = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void Update()
    {
        playerMoveAmount = playerMoveAction.action.ReadValue<Vector2>();

        if (playerJumpAction.action.WasPressedThisFrame())
            jumpRequested = true;
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (isGrounded)
            jumpsRemaining = 1;

        // Direction de la caméra
        Vector3 cameraForward = playerCamera != null ? playerCamera.forward : Vector3.forward;

        cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;

        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;

        Vector3 moveDirection = cameraForward * playerMoveAmount.y + cameraRight * playerMoveAmount.x;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // Déplacement
        Vector3 planarVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        Vector3 targetVelocity = moveDirection * playerSpeed;

        Vector3 velocityChange = targetVelocity - planarVelocity;

        velocityChange = Vector3.ClampMagnitude(velocityChange, acceleration * Time.fixedDeltaTime);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Rotation de la boule
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Vector3 rollingAxis = Vector3.Cross(Vector3.up, moveDirection).normalized;

            rb.AddTorque(rollingAxis * rollingTorque,ForceMode.Acceleration);
        }

        // Double saut
        if (jumpRequested)
        {
            if (jumpsRemaining > 0)
            {
                float verticalSpeed = rb.linearVelocity.y;

                if (verticalSpeed < 0f)
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

                jumpsRemaining--;

                jumpSound.Play();
            }

            jumpRequested = false;
        }
    }

    private void CheckGround()
    {
        float radius = sphereCollider.radius * Mathf.Max(transform.lossyScale.x,Mathf.Max(transform.lossyScale.y, transform.lossyScale.z));
        isGrounded = Physics.Raycast(transform.position,Vector3.down,radius + groundCheckDistance,groundLayers,QueryTriggerInteraction.Ignore);
    }

    public void SetSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }
}
