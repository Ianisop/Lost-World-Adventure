using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnGameOver()
    {
        gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level");
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
