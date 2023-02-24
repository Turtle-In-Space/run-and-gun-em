using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private Animator explosion;

    private void Start()
    {
        explosion.Play("Explosion");
    }
}
