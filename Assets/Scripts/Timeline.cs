using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public int timelineLength;

    List<Vector2> timeline = new List<Vector2>();
    PlayerMovement movementController;
    UserInput inputController;

    [HideInInspector] public bool isRewinding = false;

    void Awake()
    {
        movementController = FindObjectOfType<PlayerMovement>();
        inputController = FindObjectOfType<UserInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogMove(Vector2 move)
    {
        if (isRewinding) { return; }

        timeline.Add(move);

        if (timeline.Count >= timelineLength)
        {
            StartRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        inputController.userControlsMovement = false;
        StartCoroutine(RewindMovement());
    }

    IEnumerator RewindMovement()
    {
        yield return new WaitForSeconds(.8f);
        for (int i=0; i<timeline.Count; i++)
        {
            if (isRewinding)
            {
                Vector2 move = timeline[i];
                timeline[i] = Vector2.zero;
                movementController.Move(move);
                yield return new WaitForSeconds(.4f);
            }
        }
        timeline = new List<Vector2>();
        EndRewind();
    }

    void EndRewind()
    {
        inputController.userControlsMovement = true;
        isRewinding = false;
    }

    public List<Vector2> GetTimeline()
    {
        return timeline;
    }

    public void StopRewind()
    {
        StopAllCoroutines();
    }
}
