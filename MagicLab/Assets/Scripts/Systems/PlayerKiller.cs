using UnityEngine;

public class PlayerKiller : MonoBehaviour
{

    [SerializeField] private WinDefeatManager winDefeatManager;

    public void OnTriggerEnter(Collider other)
    {
        PlayerTarget playerTarget = other.GetComponent<PlayerTarget>();
        if (playerTarget)
        {
            winDefeatManager.ShowDefeatPicture();
        }
    }
}
