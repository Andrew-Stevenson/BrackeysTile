using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineDisplay : MonoBehaviour
{
    Timeline timeline;
    [SerializeField] GameObject timelineIcon;
    [SerializeField] Sprite ArrowIcon;
    [SerializeField] Animator hourglass;

    List<GameObject> timelineIcons = new List<GameObject>();

    bool startedRewind = false;

    // Start is called before the first frame update
    void Awake()
    {
        timeline = FindObjectOfType<Timeline>();
        for (int i=0; i<timeline.timelineLength; i++)
        {
            timelineIcons.Add(Instantiate(timelineIcon, transform) as GameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!timeline.isRewinding)
        {
            startedRewind = false;
        }

        else if (!startedRewind)
        {
            startedRewind = true;
            hourglass.SetTrigger("Spin");
        }

        List<Vector2> timelineList = timeline.GetTimeline();
        for (int i=0; i < timeline.timelineLength; i++)
        {
            if (i < timelineList.Count)
            {
                Vector2 move = timelineList[i];
                Image moveDisplay = timelineIcons[i].GetComponent<Image>();
                print("hi");
                if (move == Vector2.up)
                {
                    moveDisplay.sprite = ArrowIcon;
                    moveDisplay.rectTransform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                }
                else if (move == Vector2.down)
                {
                    moveDisplay.sprite = ArrowIcon;
                    moveDisplay.rectTransform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                }
                else if (move == Vector2.left)
                {
                    moveDisplay.sprite = ArrowIcon;
                    moveDisplay.rectTransform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                }
                else if (move == Vector2.right)
                {
                    moveDisplay.sprite = ArrowIcon;
                    moveDisplay.rectTransform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                }
            }
            else
            {

            }
        }
    }
}
