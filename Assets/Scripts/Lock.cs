using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public void OpenLock()
    {
        GetComponent<Animator>().SetTrigger("Open");
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void RemoveLock()
    {
        Destroy(gameObject);
    }
}
