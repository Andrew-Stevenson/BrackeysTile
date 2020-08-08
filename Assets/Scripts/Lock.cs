using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public void OpenLock()
    {
        StartCoroutine(PlayAndDestroy());
    }

    IEnumerator PlayAndDestroy()
    {
        GetComponent<Animator>().SetTrigger("Open");
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
