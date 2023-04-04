using System.Collections;
using UnityEngine;

public class BlodParticle : MonoBehaviour
{
    private void Start()
    {
        IEnumerator coroutine = RemoveWhenDone();
        StartCoroutine(coroutine);
    }

    /*
     * Tar bort gO
     */
    private IEnumerator RemoveWhenDone()
    {
        yield return new WaitForSeconds(12f);
        Destroy(gameObject);
    }
}
