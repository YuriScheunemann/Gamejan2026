Assets\Scripts\TrashPuzzleScripts\TrashItem.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public TrashType type;
    public float conveyorSpeed = 2f;
    public float fallGravityScale = 1f;
    [Tooltip("Margem em viewport fora da qual o item é destruído")]
    public float offscreenMargin = 0.1f;

    private Camera mainCamera;
    private bool isDragging;
    private Vector3 dragOffset;
    private Rigidbody2D rb;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("Main Camera não encontrada. Marque a câmera como MainCamera (tag).");

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }

        // Inicialmente kinematic para permitir mover pela esteira via transform
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = fallGravityScale;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // movimentação da esteira quando não está sendo arrastado e está kinematic
        if (!isDragging && rb.bodyType == RigidbodyType2D.Kinematic)
        {
            transform.Translate(Vector3.down * conveyorSpeed * Time.deltaTime);
        }

        // destruir caso saia da tela (viewport)
        if (mainCamera != null)
        {
            Vector3 vp = mainCamera.WorldToViewportPoint(transform.position);
            if (vp.x < -offscreenMargin || vp.x > 1f + offscreenMargin ||
                vp.y < -offscreenMargin || vp.y > 1f + offscreenMargin)
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector3 ScreenToWorld(Vector2 screenPos)
    {
        Vector3 screen = screenPos;
        float zDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        screen.z = zDistance;
        return mainCamera.ScreenToWorldPoint(screen);
    }

    // Inicia arrastar ao pressionar (segurar para mover)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Vector3 mouseWorld = ScreenToWorld(eventData.position);
            Vector2 mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);

            Collider2D hit = Physics2D.OverlapPoint(mouseWorld2D);
            if (hit != null)
            {
                TrashItem hitItem = hit.GetComponentInParent<TrashItem>();
                if (hitItem == this)
                {
                    isDragging = true;
                    // garante comportamento non-physical enquanto arrasta
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    rb.bodyType = RigidbodyType2D.Kinematic;

                    dragOffset = transform.position - mouseWorld;
                    dragOffset.z = 0f;
                }
            }
        }
    }

    // Movimenta enquanto o dedo/mouse arrasta
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        Vector3 mouseWorld = ScreenToWorld(eventData.position);
        mouseWorld.z = transform.position.z;

        // Usar MovePosition para respeitar física kinematic
        if (rb != null)
            rb.MovePosition(mouseWorld + dragOffset);
        else
            transform.position = mouseWorld + dragOffset;
    }

    // Ao soltar o botão, solta o objeto e ele passa a cair (physics)
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
            Drop();
    }

    private void Drop()
    {
        isDragging = false;
        // habilita física para cair
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallGravityScale;
        rb.freezeRotation = true;

        // se já estiver dentro de uma lixeira no momento do drop, checa imediatamente
        CheckBin();
    }

    // Checagem imediata (caso o item seja solto já dentro da área da lixeira)
    private void CheckBin()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            TrashBin bin = collider.GetComponent<TrashBin>();

            if (bin != null)
            {
                bin.ReceiveTrash(this);
                return;
            }
        }
    }

    // Quando colidir com uma lixeira enquanto estiver caindo, entrega automaticamente
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TrashBin bin = collision.collider.GetComponent<TrashBin>();
        if (bin != null)
        {
            bin.ReceiveTrash(this);
        }
    }

    // Caso a lixeira use trigger em vez de colisão física
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashBin bin = collision.GetComponent<TrashBin>();
        if (bin != null)
        {
            bin.ReceiveTrash(this);
        }
    }
}