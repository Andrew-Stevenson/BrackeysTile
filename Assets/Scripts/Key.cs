using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] AudioClip unlockSFX;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] Sprite open;

    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var tileOffest = new Vector3(0.4f, 0.4f, 0);
        Collider2D hit = Physics2D.OverlapArea(transform.position + tileOffest, transform.position - tileOffest, playerLayer);
        if (hit)
        {
        }
        if (hit && !player.isMoving)
        {
            SoundsManager.instance.Play(unlockSFX);            
            foreach (Lock aLock in FindObjectsOfType<Lock>())
            {
                aLock.OpenLock();
            }
            GetComponent<SpriteRenderer>().sprite = open;
        }
    }
}
