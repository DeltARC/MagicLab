using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private ScoreDatas scoreData;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager SoundManager;

    private void OnTriggerEnter(Collider other)
    {
        Collectible Target = other.gameObject.GetComponent<Collectible>();
        if (Target)
        {
            Crystal crystal = other.gameObject.GetComponent<Crystal>();
            if (crystal)
            {
                scoreData.Money = scoreData.Money + Target.scoreValue;
                Debug.Log(scoreData.Money);
                uiManager.UpdateScoreText(scoreData.Money);
                SoundManager.PlayCrystalSound();
            }

            Life life = other.gameObject.GetComponent<Life>();
            if (life)
            {
                scoreData.Life += 1;
                Debug.Log(scoreData.Life);
                uiManager.UpdateLifeText(scoreData.Life);
                SoundManager.PlayLifeSound();
            }
            Destroy(other.gameObject);
        }
    }
}
