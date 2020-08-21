using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Animator menuAnimator;
    public AudioClip buttonMouseOverSound;

    bool buttonsEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            DisplayMenu();
        }
    }

    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(5);
        DisplayMenu();
    }

    IEnumerator DelayButtonActive()
    {
        yield return new WaitForSeconds(2.7f);
        buttonsEnabled = true;
    }

    void DisplayMenu()
    {
        StopAllCoroutines();
        menuAnimator.SetTrigger("ShowMenu");
        StartCoroutine(DelayButtonActive());
    }

    public void StartGame()
    {
        GameManager.instance.LoadFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SkipAnimation()
    {
        menuAnimator.SetBool("SkipAnimation", true);
        buttonsEnabled = true;
    }

    public void PlayButtonMouseoverSound()
    {
        if (!buttonsEnabled) { return; }
        if (SoundsManager.instance)
        {
            SoundsManager.instance.Play(buttonMouseOverSound);
        }
    }
}
