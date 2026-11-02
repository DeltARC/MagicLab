using UnityEngine;

public class WinObject : MonoBehaviour
{
    [SerializeField] private WinDefeatManager winDefeatManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter(Collider other)
    {
        PlayerTarget playerTarget = other.GetComponent<PlayerTarget>();
        if (playerTarget)
        {
            winDefeatManager.ShowWinPicture();
        }
    }
}
