using System.Collections;
using UnityEngine;

public class BlodParticle : MonoBehaviour
{
    private void Start()
    {
        IEnumerator coroutine = RemoveWhenDone();
        StartCoroutine(coroutine);
    }

    private IEnumerator RemoveWhenDone()
    {
        yield return new WaitForSeconds(12f);
        Destroy(gameObject);
    }
}
