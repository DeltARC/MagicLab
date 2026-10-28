using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    public void LoadNewScene() 
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerTarget playerTarget = other.GetComponent<PlayerTarget>();
        if (playerTarget)
        {
            LoadNewScene();
        }
    }
}
