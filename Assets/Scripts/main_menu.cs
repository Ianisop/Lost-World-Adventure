using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public Button start, quit;
    public Image fadeImage;     // Reference to the UI image used for fading

    public float fadeTime = 2f; // Duration of the fade

    private bool fading = false; // Flag to prevent multiple fades at once

    void Start()
    {
        start.onClick.AddListener(startAction);
        quit.onClick.AddListener(quitAction);
        fadeImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fading)
        {
            fadeImage.enabled = true;
        }
        if (!fading)
        {
            fadeImage.enabled = false;
        }
    }


    void startAction()
    {
        Debug.Log("hit");
        FadeToScene("GameScene");
    }

    void quitAction()
    {
        Application.Quit();
    }


    public void FadeToScene(string sceneName)
    {
        if (!fading)
        {
            StartCoroutine(FadeOutAndIn(sceneName));
        }
    }

    private IEnumerator FadeOutAndIn(string sceneName)
    {
        fading = true;

        // Fade to black
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(0f);
        fadeImage.CrossFadeAlpha(1f, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Fade back to normal color
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(1f);
        fadeImage.CrossFadeAlpha(0f, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);

        fading = false;
    }
}
