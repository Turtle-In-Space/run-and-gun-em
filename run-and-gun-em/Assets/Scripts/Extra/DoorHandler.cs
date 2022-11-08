using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private bool isEPressed;

    private void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEPressed)
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
