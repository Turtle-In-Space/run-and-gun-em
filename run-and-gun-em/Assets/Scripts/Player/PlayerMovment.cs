using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 10;
    private new Camera camera;    
    private new Rigidbody2D rigidbody;
    private Animator animator;  

    Vector2 speed;
    Vector2 mousePos;


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
        rigidbody.MovePosition(rigidbody.position + speed * moveSpeed * Time.deltaTime);

        Vector2 lookDirection = mousePos - rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
    }
}
