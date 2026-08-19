using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMoviment : MonoBehaviour
{
    [Header("TopDown")]
    Rigidbody2D rb;
    Vector2 movementInput;
    public AudioClip[] sonsDePassos;
    AudioSource audioSource;
    Coroutine passosCoroutine;
    bool isMoving = false;
    int passoIndex = 0;
    private Animator currentAnimator;

    [Header("Platform")]
   // public float moveSpeed = 5f;
    public float jumpForce = 10f;   
    private Animator anim;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [SerializeField] private bool isTopdown;
    [SerializeField] private float speed = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        UpdateAnimator();
    }

    void Update()
    {
        if (currentAnimator == null) UpdateAnimator();

        if (movementInput != Vector2.zero && isTopdown)
        {
            currentAnimator.SetFloat("Horizontal", movementInput.x);
            currentAnimator.SetFloat("Vertical", movementInput.y);
            currentAnimator.SetFloat("Speed", movementInput.sqrMagnitude);
        }
        else if(movementInput != Vector2.zero && !isTopdown)
        {
            currentAnimator.SetFloat("Horizontal", movementInput.x);
            currentAnimator.SetFloat("Speed", movementInput.sqrMagnitude);
        }
        else
        {
            currentAnimator.SetFloat("Speed", 0);
        }
        if (!isTopdown)
        {
            Move();
            Jump();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        bool currentlyMoving = movementInput != Vector2.zero;

        if (currentlyMoving && !isMoving)
        {
            isMoving = true;
            passosCoroutine = StartCoroutine(TocarPassosEmOrdem());
        }
        else if (!currentlyMoving && isMoving)
        {
            isMoving = false;
            if (passosCoroutine != null)
                StopCoroutine(passosCoroutine);
        }
    }

    IEnumerator TocarPassosEmOrdem()
    {
        passoIndex = 0;

        while (true)
        {
            float delay = 0.25f;

            if (sonsDePassos.Length > 0)
            {
                audioSource.volume = 0.3f;
                audioSource.PlayOneShot(sonsDePassos[passoIndex]);

                passoIndex++;
                if (passoIndex >= sonsDePassos.Length)
                    passoIndex = 0;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void FixedUpdate()
    {
                      
        rb.linearVelocity = movementInput * speed;
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h*speed,rb.linearVelocity.y);
       
        if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Olhando para a direita
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Olhando para a esquerda
        }
        anim.SetFloat("Horizontal", h);

        bool currentlyMoving = movementInput != Vector2.zero;

        if (currentlyMoving && !isMoving)
        {
            isMoving = true;
            passosCoroutine = StartCoroutine(TocarPassosEmOrdem());
        }
        else if (!currentlyMoving && isMoving)
        {
            isMoving = false;
            if (passosCoroutine != null)
                StopCoroutine(passosCoroutine);
        }
       
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)        
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);        

        anim.SetBool("IsJumping", !isGrounded);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    public void UpdateAnimator()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                currentAnimator = child.GetComponent<Animator>();
                break;
            }
        }
    }

}
