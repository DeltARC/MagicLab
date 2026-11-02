using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDefeatManager : MonoBehaviour
{
    [SerializeField] private GameObject winPicture;
    [SerializeField] private GameObject defeatPicture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinPicture()
    {
        winPicture.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowDefeatPicture()
    {
        defeatPicture.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BonusLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
