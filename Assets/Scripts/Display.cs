using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    Timeline timeline;

    [SerializeField] GameObject timelineDisplay;
    [SerializeField] GameObject timelineArrow;
    [SerializeField] Transform arrowHolder;
    [SerializeField] GameObject hourglass;
    [Space]
    [SerializeField] Sprite arrow;
    [SerializeField] Sprite noMove;


    Animator hourglassAnimator;

    [SerializeField] float spacingOffset = 1.25f;

    bool startedRewind = false;

    // Start is called before the first frame update
    void Awake()
    {

        timeline = GetComponentInParent<Timeline>();

        SpriteRenderer sr = timelineDisplay.GetComponent<SpriteRenderer>();
        sr.size = new Vector2(3 + spacingOffset + (timeline.timelineLength * spacingOffset), sr.size.y);

        hourglass.transform.position = new Vector3((-timeline.timelineLength / 2f)*spacingOffset - 0.6f, hourglass.transform.position.y);

        hourglassAnimator = hourglass.GetComponent<Animator>();

        List<Vector2> timelineList = timeline.GetTimeline();
        for (int i = 0; i < timeline.timelineLength; i++)
        {
            GameObject arrowIcon = Instantiate(timelineArrow, arrowHolder);
            arrowIcon.transform.position = new Vector3((i * spacingOffset) - ((timeline.timelineLength / 2f) * spacingOffset) + 2 + (spacingOffset / 7), arrowIcon.transform.position.y);
        }
    }

    void Update()
    {

        if (!timeline.isRewinding)
        {
            startedRewind = false;
        }

        else if (!startedRewind)
        {
            startedRewind = true;
            hourglassAnimator.SetTrigger("Spin");
        }

        List<Vector2> timelineList = timeline.GetTimeline();
        for (int i = 0; i < timeline.timelineLength; i++)
        {
            GameObject arrowObject = arrowHolder.GetChild(i).gameObject;
            SpriteRenderer arrowRenderer = arrowObject.GetComponent<SpriteRenderer>();
            if (i < timelineList.Count)
            {
                Vector2 move = timelineList[i];
                arrowRenderer.sprite = arrow;
                if (move == Vector2.up)
                {
                    arrowObject.transform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                }
                else if (move == Vector2.down)
                {
                    arrowObject.transform.localRotation = Quaternion.AngleAxis(270f, Vector3.forward);
                }
                else if (move == Vector2.left)
                {
                    arrowObject.transform.localRotation = Quaternion.AngleAxis(180f, Vector3.forward);
                }
                else if (move == Vector2.right)
                {
                    arrowObject.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
                }
                else
                {
                    arrowRenderer.sprite = noMove;
                    arrowObject.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
                }
            }
            else
            {
                arrowRenderer.sprite = noMove;
                arrowObject.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
            }
        }
    }
}
