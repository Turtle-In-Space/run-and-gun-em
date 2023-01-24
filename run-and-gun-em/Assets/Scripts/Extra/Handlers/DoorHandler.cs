using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    public bool canExplode = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canExplode && Input.GetKey(KeyCode.E))
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
