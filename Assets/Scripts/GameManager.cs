using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public int currentLevel = 1;
    public int endLevel = 1;
    public bool levelComplete = false;
    public bool isReloading = false;


    private void Start()
    {
        if (SceneManager.sceneCount < 2)
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
    }

    public void RestartLevel()
    {
        if (isReloading)
            return;

        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        isReloading = true;

        yield return new WaitForSeconds(.6f);

        Fader.instance.FadeOut();

        yield return new WaitForSeconds(.6f);

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

        while (!unload.isDone)
        {
            yield return 0;
        }

        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);

        Fader.instance.FadeIn();

        isReloading = false;
    }

    public void CompleteLevel()
    {
        if (levelComplete)
            return;

        levelComplete = true;

        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);

        Fader.instance.FadeOut();

        yield return new WaitForSeconds(.6f);


        if (endLevel == currentLevel)
        {
            //SceneManager.LoadScene(currentLevel + 1, LoadSceneMode.Single);           
        }
        else
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

            while (!unload.isDone)
            {
                yield return 0;
            }

            currentLevel++;
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
        }

        Fader.instance.FadeIn();

        levelComplete = false;
    }
}
