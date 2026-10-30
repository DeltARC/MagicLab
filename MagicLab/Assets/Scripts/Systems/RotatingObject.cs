using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private float rotationY = 0f;
    [SerializeField] private float rotationX = 0f;
    [SerializeField] private float rotationZ = 0f;
    void Update()
    {
        transform.Rotate(rotationX * Time.deltaTime, rotationY * Time.deltaTime, rotationZ * Time.deltaTime);
    }
}
