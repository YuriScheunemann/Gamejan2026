using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BoatMoviment : MonoBehaviour
{
    Rigidbody2D rb;    
    [SerializeField] private float _speed = 2;
    private float _moviment;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moviment * _speed,0);
    }

    private void Update()
    {
        _moviment = Input.GetAxis("Horizontal");       
    }
}
