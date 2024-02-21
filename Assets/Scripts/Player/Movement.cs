using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 8f;
    public float jumpStrength = 24f;
    public float gravityScale = 1f;
    public float jumpInputDelay = 0.1f;
    public Vector2 speedOnVine;

    public float boxCastDistance = 0.01f;
    public Transform groundCheck;
    public Vector2 boxCastSize;
    public LayerMask groundMask;

    private bool isGrounded;
    private bool isOnVine;
    private bool inputJump;
    private bool isGrabbing;
    private float jumpTimer = 0f;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float verticalAxis;

    public Vector2 Velocity
    {
        get => velocity;
    }

    private void Awake()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (rb == null)
            Debug.LogError("Rigidbody2D not found");
        if (groundCheck == null)
            Debug.LogError("GroundCheck object not found");
    }

    private void Update()
    {
        if (inputJump)
            jumpTimer += Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            inputJump = true;
            jumpTimer = 0f;
        }
        verticalAxis = Input.GetAxisRaw("Vertical");
        if (isOnVine && verticalAxis > 0)
            isGrabbing = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vine"))
        {
            isOnVine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vine"))
        {
            isOnVine = false;
            isGrabbing = false;
        }
    }

    private void FixedUpdate()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        bool hit = Physics2D.BoxCast(groundCheck.position, boxCastSize, 0, Vector2.down, boxCastDistance, groundMask);
        isGrounded = hit;

        if (isGrounded)
        {
            velocity.y = 0f;
            if (inputJump && jumpTimer < jumpInputDelay)
            {
                inputJump = false;
                jumpTimer = 0f;
                velocity.y = jumpStrength;
            }
        }
        else if (isOnVine && isGrabbing)
        {
            if (verticalAxis != 0)
            {
                velocity.y = speedOnVine.y * verticalAxis;
            }
            else if (inputJump && jumpTimer < jumpInputDelay)
            {
                inputJump = false;
                jumpTimer = 0f;
                isGrabbing = false;
                velocity.y = jumpStrength;
            }
            else
                velocity.y = 0f;
        }
        else
        {
            velocity.y += Physics2D.gravity.y * gravityScale * Time.deltaTime;
        }

        if (isOnVine && isGrabbing)
            velocity.x *= speedOnVine.x;
        else
            velocity.x *= speed;

        rb.MovePosition((Vector2)transform.position + velocity * Time.deltaTime);
    }
}
