using UnityEngine;

public class UnlockPortal : MonoBehaviour
{

    [SerializeField] private GameObject portal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerTarget playerTarget = other.GetComponent<PlayerTarget>();
        if (playerTarget)
        {
            portal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
