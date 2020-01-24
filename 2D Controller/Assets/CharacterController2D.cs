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
    private Vector3 m_Velocity = Vector3.zero;
    public float MovementSmoothing = .05f;
    public Collider2D CrouchColliderDisable;
    public float CrouchSpeed;
    public bool IsCrouchButton;

    private void Start()
    {
        // setting the rigidbody to this object rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
        CrouchColliderDisable = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // is the jump key down?
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump = true;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2f - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            CrouchColliderDisable.enabled = false;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            CrouchColliderDisable.enabled = true;
        }

    }
   
    private void FixedUpdate()
    {
        // movement
        Vector3 targetVelocity = new Vector2(Speed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

        // jumping
        if (Jump && isGrounded)
        {
            Jump = false;
            isGrounded = false;
            rb.velocity = Vector2.up * JumpForce * Time.fixedDeltaTime;
        }

        // is grounded?
        isGrounded = Physics2D.OverlapArea(new Vector2(GroundCheck.transform.position.x - 0.5f, GroundCheck.transform.position.y), new Vector2(GroundCheck.transform.position.x + 0.5f, GroundCheck.transform.position.y - 0.05f), Ground);

        

    }
}
