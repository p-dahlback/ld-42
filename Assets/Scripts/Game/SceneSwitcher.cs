using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour {

    public Image overlay;
    public float fadeTime;

    void Awake()
    {
        if (GetActiveSceneIndex() != 0)
        {
            StartCoroutine("FadeToClear");
        }
    }

    public void LoadNextSceneWithDelay()
    {
        StartCoroutine("FadeToBlackAndLoadScene", GetActiveSceneIndex() + 1);
    }

    public void ReloadSceneWithDelay()
    {
        StartCoroutine("FadeToBlackAndLoadScene", GetActiveSceneIndex());
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    private IEnumerator FadeToClear()
    {
        overlay.gameObject.SetActive(true);
        overlay.color = Color.black;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            overlay.color = Color.Lerp(Color.black, Color.clear, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        overlay.color = Color.clear;
        overlay.gameObject.SetActive(false);
    }

    private IEnumerator FadeToBlackAndLoadScene(int sceneIndex)
    {
        overlay.gameObject.SetActive(true);
        overlay.color = Color.clear;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            overlay.color = Color.Lerp(Color.clear, Color.black, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        overlay.color = Color.black;
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator ReloadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("FadeToBlack");
    }

    private int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
