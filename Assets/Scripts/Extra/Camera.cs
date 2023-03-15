using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target)
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}