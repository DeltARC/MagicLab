using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }
}
