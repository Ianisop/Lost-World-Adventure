using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] OptionsMenu options;

    private void Start()
    {
        PlayerControlManager.instance.OnPause += (context)=> OnEscapePressed();
        menu.SetActive(false);
    }

    public void OnEscapePressed()
    {
        if (menu.activeSelf)
            OnResumeClicked();
        else
            OnPause();
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void OnResumeClicked()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        options.CloseOptionMenu();
    }

    public void OnOptionsClicked()
    {
        options.OpenOptionsMenu();
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
