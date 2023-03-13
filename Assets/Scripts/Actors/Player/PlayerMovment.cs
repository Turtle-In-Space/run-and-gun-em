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

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /*
     * Tar movment inputs och direction
     */
    private void GetInputs()
    {
        speed.x = Input.GetAxisRaw("Horizontal");
        speed.y = Input.GetAxisRaw("Vertical");
        speed.Normalize();

        animator.SetFloat("Speed", speed.magnitude);
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    /*
     * Flyttar spelare med speed
     * Vänder spelare mot musen
     */
    private void Move()
    {
        rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * speed);

        Vector2 lookDirection = mousePos - rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
    }
}
