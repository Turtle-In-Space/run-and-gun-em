using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        if (target)
        {
            transform.position = target.position;
        }
    }
}
