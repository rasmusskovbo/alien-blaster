using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int playerScore;
    private static ScoreKeeper instance;

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ModifyScore(int value)
    {
        playerScore += value;
        Mathf.Clamp(playerScore, 0, int.MaxValue);
    }
    
    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }
}
