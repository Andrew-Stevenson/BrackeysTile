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

    public int firstLevel = 2;
    public int currentLevel = 1;
    public int numberOfLevels = 1;
    public bool levelComplete = false;
    public bool isReloading = false;
    public Camera mainCamera;


    private void Start()
    {
        if (SceneManager.sceneCount < 2) {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            mainCamera.enabled = (false);
        }
        else
        {
            currentLevel = Mathf.Max(SceneManager.GetSceneAt(1).buildIndex, SceneManager.GetSceneAt(0).buildIndex);
        }
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

        StartCoroutine(LoadNextLevel(currentLevel + 1));
    }

    IEnumerator LoadNextLevel(int nextLevel, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);

        Fader.instance.FadeOut();

        yield return new WaitForSeconds(.6f);

        if (nextLevel >= firstLevel)
        {
            mainCamera.enabled = (true);
        } else
        {
            mainCamera.enabled = (false);
        }

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

        while (!unload.isDone)
        {
            yield return 0;
        }

        currentLevel = nextLevel;
        AsyncOperation load = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return 0;
        }

        if (firstLevel + numberOfLevels == currentLevel)
        {
            mainCamera.enabled = (false);
            StartCoroutine(LoadMainMenu(5));
        }

        Fader.instance.FadeIn();

        levelComplete = false;
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMainMenu(0));
    }

    IEnumerator LoadMainMenu(float delay)
    {
        yield return new WaitForSeconds(delay);

        Fader.instance.FadeOut();

        yield return new WaitForSeconds(.6f);
        mainCamera.enabled = (true);

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);        

        while (!unload.isDone)
        {
            yield return 0;
        }

        currentLevel = 1;
        AsyncOperation load = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return 0;
        }
        mainCamera.enabled = (false);
        FindObjectOfType<MainMenu>().SkipAnimation();
        Fader.instance.FadeIn();
    }

    public void LoadFirstLevel()
    {
        currentLevel = 1;
        StartCoroutine(LoadNextLevel(firstLevel, 0));
    }
}
