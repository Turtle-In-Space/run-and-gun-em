using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    public Transform player;
    private new Camera camera;

    private Vector2 enemyPosition;
    private Plane[] planes = new Plane[6];
    readonly float offset = 2;


    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        enemyPosition = transform.position;
    }

    private void Update()
    {
        Vector2 direction = (enemyPosition - (Vector2)player.position).normalized;
        Ray ray = new(player.position, direction);
        GeometryUtility.CalculateFrustumPlanes(camera, planes);
        float minDistance = float.MaxValue;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (Mathf.Abs(distance) < minDistance)
                {
                    minDistance = distance;
                }
            }
        }

        minDistance -= offset;
        transform.position = (Vector2)player.position + minDistance * direction;
    }
}