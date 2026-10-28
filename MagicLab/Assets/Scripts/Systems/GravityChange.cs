using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GravityChange : MonoBehaviour
{
    [SerializeField] private float GravityX = 0f;
    [SerializeField] private float GravityY = -9.81f;
    [SerializeField] private float GravityZ = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider other)
    {
        PlayerTarget Target = other.gameObject.GetComponent<PlayerTarget>();
        if (Target)
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement)
            {
                playerMovement.SetGravity(new Vector3(GravityX, GravityY, GravityZ));
                changeGravity();
            }
        }
    }

    void changeGravity()
    {
        Physics.gravity = new Vector3(GravityX, GravityY, GravityZ);
    } 
}
