using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /*
     * Följer target
     * Behåller pos.z
     */
    private void Update()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}