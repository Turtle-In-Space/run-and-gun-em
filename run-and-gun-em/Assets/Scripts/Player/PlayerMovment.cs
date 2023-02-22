using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private new Camera camera;    
    private new Rigidbody2D rigidbody;
    private Animator animator;  

    private Vector2 speed;
    private Vector2 mousePos;

    private readonly int moveSpeed = 11;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        speed.x = Input.GetAxisRaw("Horizontal");
        speed.y = Input.GetAxisRaw("Vertical");
        speed.Normalize();

        animator.SetFloat("Speed", speed.magnitude);
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * speed);

        Vector2 lookDirection = mousePos - rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
    }
}
