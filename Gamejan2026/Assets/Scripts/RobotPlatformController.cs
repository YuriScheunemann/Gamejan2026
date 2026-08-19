using UnityEngine;

public class RobotPlatformController : MonoBehaviour
{
    [Header("Movimento")]
    [Tooltip("Velocidade horizontal do rob�.")]
    public float speed = 6f;

    [Tooltip("For�a do pulo (valor aplicado na velocidade Y).")]
    public float jumpForce = 12f;

    [Header("Detec��o do ch�o")]
    [Tooltip("Transform localizado nos p�s do rob� (para checar se est� no ch�o).")]
    public Transform groundCheck;
    [Tooltip("Raio usado na checagem do ch�o.")]
    public float groundCheckRadius = 0.12f;
    [Tooltip("Layer(s) que representam o ch�o.")]
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Entrada horizontal: suporta A/D e eixo Horizontal
        if (Input.GetKey(KeyCode.A))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            horizontalInput = 1f;
        else
            horizontalInput = Input.GetAxisRaw("Horizontal");

        // Pular com W
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            Jump();
        }

        // Flip simples dependendo da dire��o
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;
        vel.x = horizontalInput * speed;
        rb.linearVelocity = vel;
    }

    private bool IsGrounded()
    {
        if (groundCheck != null)
        {
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // Fallback: pequeno raycast para baixo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, groundLayer);
        return hit.collider != null;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.15f);
        }
    }
}