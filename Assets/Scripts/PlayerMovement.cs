using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform playerGFX;

    [Space]
    [Header("Layer Masks")]
    [SerializeField] LayerMask whatIsFloor;
    [SerializeField] LayerMask whatIsWin;
    [SerializeField] LayerMask whatBlocksMovement;

    [Space]
    [Header("SFX")]
    [SerializeField] AudioClip moveSFX;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] AudioClip winSFX;

    [HideInInspector] public bool isMoving = false;
    bool isFacingRight = true;
    Vector2 movePoint = new Vector2(0, 0);

    Timeline timeline;
    UserInput inputController;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movePoint = transform.position;

        timeline = FindObjectOfType<Timeline>();
        inputController = GetComponent<UserInput>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    public void Move(Vector2 direction)
    {
        if (isMoving) { return; }

        if (Physics2D.Raycast(transform.position, direction, 1, whatBlocksMovement))
        {
            return;
        }

        SoundsManager.instance.Play(moveSFX);

        timeline.LogMove(direction);

        isMoving = true;

        animator.SetTrigger("Make Move");

        movePoint += direction;

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            FlipSprite();
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint) <= Mathf.Epsilon && isMoving)
        {
            PreformEndOfMoveActions();
        }
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = playerGFX.localScale;
        theScale.x *= -1;
        playerGFX.localScale = theScale;
    }

    void PreformEndOfMoveActions()
    {
        transform.position = movePoint;
        isMoving = false;
        if (isLevelComplete())
        {
            Win();
        }
        else if (isPlayerDead())
        {
            Die();
        }
    }

    bool isPlayerDead()
    {
        var tileOffest = new Vector3(0.4f, 0.4f, 0);
        Collider2D hit = Physics2D.OverlapArea(transform.position + tileOffest, transform.position - tileOffest, whatIsFloor);
        if (!hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isLevelComplete()
    {
        var tileOffest = new Vector3(0.4f, 0.4f, 0);
        Collider2D hit = Physics2D.OverlapArea(transform.position + tileOffest, transform.position - tileOffest, whatIsWin);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Die()
    {
        SoundsManager.instance.Play(dieSFX, true);
        timeline.StopRewind();
        inputController.userControlsMovement = false;
        animator.SetBool("IsDead", true);
        GameManager.instance.RestartLevel();
    }

    void Win()
    {
        SoundsManager.instance.Play(winSFX, true);
        timeline.StopRewind();
        inputController.userControlsMovement = false;
        animator.SetTrigger("Win");
        GameManager.instance.CompleteLevel();
    }
}
