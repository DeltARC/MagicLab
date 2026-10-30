using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lifeText;

    private void Start()
    {
    }
    public void UpdateScoreText(int scoreValue)
    {
        scoreText.text = scoreValue.ToString();
    }

    public void UpdateLifeText(int lifeValue)
    {
        lifeText.text = lifeValue.ToString();
    }

}