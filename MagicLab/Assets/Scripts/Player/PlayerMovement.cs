using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float rollingTorque = 8f;
 
    [Header("Gravity")]
    [SerializeField] private Vector3 gravity =
        new Vector3(0f, -9.81f, 0f);
 
    [Header("Ground Detection")]
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayers = ~0;
 
    [Header("Input")]
    [SerializeField] private InputActionReference playerMoveAction;
    [SerializeField] private InputActionReference playerJumpAction;
    [SerializeField] private Transform playerCamera;
 
    private Rigidbody rb;
    private SphereCollider sphereCollider;
 
    private Vector2 playerMoveAmount;
    private bool jumpRequested;
    private bool isGrounded;
 
    private Vector3 GravityUp => -gravity.normalized;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
 
        // On gère nous-mêmes la gravité directionnelle.
        rb.useGravity = false;
 
        // Améliore la stabilité du mouvement physique.
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;
    }
 
    private void Update()
    {
        playerMoveAmount =
            playerMoveAction.action.ReadValue<Vector2>();
 
        if (playerJumpAction.action.WasPressedThisFrame())
        {
            jumpRequested = true;
        }
    }
 
    private void FixedUpdate()
    {
        if (gravity.sqrMagnitude < 0.001f)
            return;
 
        Vector3 up = GravityUp;
 
        // Gravité personnalisée.
        rb.AddForce(gravity, ForceMode.Acceleration);
 
        CheckGround();
 
        // Direction de la caméra projetée sur le plan du sol.
        Vector3 cameraForward = playerCamera != null
            ? playerCamera.forward
            : Vector3.forward;
 
        cameraForward =
            Vector3.ProjectOnPlane(cameraForward, up).normalized;
 
        if (cameraForward.sqrMagnitude < 0.001f)
        {
            cameraForward =
                Vector3.ProjectOnPlane(Vector3.forward, up).normalized;
        }
 
        if (cameraForward.sqrMagnitude < 0.001f)
        {
            cameraForward =
                Vector3.ProjectOnPlane(Vector3.right, up).normalized;
        }
 
        Vector3 cameraRight =
            Vector3.Cross(up, cameraForward).normalized;
 
        Vector3 moveDirection =
            cameraForward * playerMoveAmount.y +
            cameraRight * playerMoveAmount.x;
 
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
 
        // Contrôle de la vitesse sur le plan du sol.
        Vector3 planarVelocity =
            Vector3.ProjectOnPlane(rb.linearVelocity, up);
 
        Vector3 targetVelocity = moveDirection * playerSpeed;
 
        Vector3 velocityChange = targetVelocity - planarVelocity;
 
        velocityChange = Vector3.ClampMagnitude(
            velocityChange, acceleration * Time.fixedDeltaTime);
 
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
 
        // Fait rouler la boule dans la direction du déplacement.
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Vector3 rollingAxis =
                Vector3.Cross(up, moveDirection).normalized;
 
            rb.AddTorque(
                rollingAxis * rollingTorque,
                ForceMode.Acceleration);
        }
 
        // Saut dans la direction opposée à la gravité.
        if (jumpRequested)
        {
            if (isGrounded)
            {
                float currentUpSpeed =
                    Vector3.Dot(rb.linearVelocity, up);
 
                if (currentUpSpeed < 0f)
                    rb.linearVelocity -= up * currentUpSpeed;
 
                rb.AddForce(
                    up * Mathf.Sqrt(2f * gravity.magnitude * jumpHeight),
                    ForceMode.VelocityChange);
            }
 
            jumpRequested = false;
        }
    }
 
    private void CheckGround()
    {
        Vector3 up = GravityUp;
 
        float radius = sphereCollider.radius *
                       Mathf.Max(
                           transform.lossyScale.x,
                           Mathf.Max(
                               transform.lossyScale.y,
                               transform.lossyScale.z));
 
        // Vérifie la surface dans la direction de la gravité.
        isGrounded = Physics.Raycast(
            transform.position,
            -up,
            radius + groundCheckDistance,
            groundLayers,
            QueryTriggerInteraction.Ignore);
    }
 
    public void SetGravity(Vector3 newGravity)
    {
        if (newGravity.sqrMagnitude < 0.001f)
            return;
 
        gravity = newGravity;
    }

    public void SetSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }
}