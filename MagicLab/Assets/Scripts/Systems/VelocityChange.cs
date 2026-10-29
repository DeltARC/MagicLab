using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VelocityChange : MonoBehaviour
{
    [SerializeField] private float NewSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider other)
    {
        PlayerTarget Target = other.gameObject.GetComponent<PlayerTarget>();
        if (Target)
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement)
            {
                playerMovement.SetSpeed(NewSpeed);
            }
        }
    }
}
