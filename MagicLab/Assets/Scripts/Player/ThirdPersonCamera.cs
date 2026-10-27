using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    [Header("Camera Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float sensitivity = 0.15f;
    [SerializeField] private float minPitch = -60.0f;
    [SerializeField] private float maxPitch = 60.0f;

    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
    if (player == null)
        return;

    // Rotation horizontale uniquement
    yaw += Input.GetAxis("Mouse X") * sensitivity;

    // Appliquer la rotation
    transform.rotation = Quaternion.Euler(0f, yaw, 0f);

    // Position du pivot
    transform.position = player.position + Vector3.up * 2.5f;
    }
}

