using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    Timeline timeline;
    [SerializeField] GameObject timelineDisplay;
    [SerializeField] GameObject timelineArrow;

    // Start is called before the first frame update
    void Awake()
    {

        timeline = GetComponentInParent<Timeline>();

        SpriteRenderer sr = timelineDisplay.GetComponent<SpriteRenderer>();
        sr.size = new Vector2(4.125f + timeline.timelineLength, sr.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
