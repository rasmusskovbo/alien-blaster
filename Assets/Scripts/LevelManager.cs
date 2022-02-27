using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadDelay = 2f;
    
    public void LoadGame()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper != null)
        {
            scoreKeeper.ResetScore();        
        }
        
        SceneManager.LoadScene("GameScreen");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadGameOver()
    {
        StartCoroutine(DelayedLoading("GameOver", sceneLoadDelay));
    }

    IEnumerator DelayedLoading(String sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
