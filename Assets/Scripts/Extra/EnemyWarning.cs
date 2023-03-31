using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    private GameObject enemy;
    private Transform playerTransform;
    private new Camera camera;

    private Vector2 enemyPosition;
    private Plane[] planes = new Plane[6];
    readonly float offset = 2;


    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = transform.parent.gameObject;
        enemyPosition = enemy.transform.position;
    }

    private void Update()
    {
        if (enemy.GetComponent<Renderer>().isVisible)
        {
            enemy.GetComponent<EnemyAI>().isWarningPlaced = false;
            Destroy(gameObject);
        }
        ChangePosition();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, enemy.transform.rotation.z * -1);
    }

    /*
     * Skickar en ray mot kanterna av kameran
     * sätter sätter positonen nära rätt kant
     */
    private void ChangePosition()
    {
        Vector2 direction = (enemyPosition - (Vector2)playerTransform.position).normalized;
        Ray ray = new(playerTransform.position, direction);
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
        transform.position = (Vector2)playerTransform.position + minDistance * direction;
    }
}