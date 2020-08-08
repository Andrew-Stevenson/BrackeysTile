using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    PlayerMovement movementContoller;
    public bool userControlsMovement = true;

    // Start is called before the first frame update
    void Start()
    {
        movementContoller = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (userControlsMovement) { HandleMovementInput(); }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundsManager.instance.ToggleMusic();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SoundsManager.instance.ToggleSFX();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            movementContoller.Move(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            movementContoller.Move(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            movementContoller.Move(new Vector2(1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            movementContoller.Move(new Vector2(0, -1));
        }
    }
}
