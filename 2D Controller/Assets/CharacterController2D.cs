using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    public Rigidbody2D rb;
    public float Speed = 0f;
    bool Jump;
    bool isGrounded = true;
    Vector3 Movement;
    public float JumpForce = 0f;
    public LayerMask Ground;
    public GameObject GroundCheck;

    private void Start()
    {
        // setting the rigidbody to this object rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // is the jump key down?
        if (Input.GetKeyDown("space"))
        {
            Jump = true;
        }

    }

    private void FixedUpdate()
    {
        // movement
        Movement = new Vector3(Input.GetAxisRaw("Horizontal") * Speed * Time.fixedDeltaTime, 0f, 0f);
        transform.position += Movement;

        // jumping
        if (Jump && isGrounded)
        {
            Jump = false;
            isGrounded = false;
            rb.AddForce(new Vector2(0f, JumpForce));
        }

        // is grounded?
        isGrounded = Physics2D.OverlapArea(new Vector2(GroundCheck.transform.position.x - 0.5f, GroundCheck.transform.position.y + 0.5f), new Vector2(GroundCheck.transform.position.x + 0.5f, GroundCheck.transform.position.y - 0.1f), Ground);
    }
}
