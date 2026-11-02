using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject QuitButton;
    [SerializeField] private GameObject OptionsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        StartButton.SetActive(false);
        SettingsButton.SetActive(false);
        QuitButton.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        StartButton.SetActive(true);
        SettingsButton.SetActive(true);
        QuitButton.SetActive(true);
        OptionsMenu.SetActive(false);
    }
}
